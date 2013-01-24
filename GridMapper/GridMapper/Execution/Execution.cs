using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using GridMapper.NetworkModelObject;
using GridMapper.NetworkRepository;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.IO;
using GridMapper.NetworkRawSender;
using GridMapper.NetworkUtilities;

namespace GridMapper
{
	class NewExecution : IExecution
	{
		Option _option;
		IRepository _repository;
		OwnPacketReceiver _ownPacketReceiver;
		OwnPacketBuilder _ownPacketBuilderForArping;
		OwnPacketBuilder _ownPacketBuilderForScanPort;
		OwnPacketSender _ownPacketSender;
		ReverseDnsResolver _reverseDnsResolver;
		Task _taskForExecution;

		public event EventHandler<TaskCompletedEventArgs> TaskCompleted;

		public NewExecution( Option startupOptions )
		{
			_option = startupOptions;
			_ownPacketReceiver = new OwnPacketReceiver( _option.Arping, _option.Port, _option.TCPPort, _option.RandomUDPPort, _option.UDPPort, _option.Timeout );
			_ownPacketSender = new OwnPacketSender( _option.NbPacketToSend, _option.WaitTime );

			_ownPacketReceiver.ArpingReceived += AddArpingInRepositoryAndContinueWithRequest;
			_ownPacketReceiver.PortReceived += AddPortNumberInRepository;

			_reverseDnsResolver = new ReverseDnsResolver();
		}

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

		public void StartScan()
		{
			_ownPacketReceiver = new OwnPacketReceiver( _option.Arping, _option.Port, _option.TCPPort, _option.RandomUDPPort, _option.UDPPort, _option.Timeout );
			_ownPacketSender = new OwnPacketSender( _option.NbPacketToSend, _option.WaitTime );

			_ownPacketReceiver.ArpingReceived += AddArpingInRepositoryAndContinueWithRequest;
			_ownPacketReceiver.PortReceived += AddPortNumberInRepository;

			_repository = new Repository();
			_ownPacketReceiver.StartReceive();

			_taskForExecution = Task.Factory.StartNew( () =>
			{
				if ( _option.Arping || _option.Ping)
				{
					_ownPacketBuilderForArping = new OwnPacketBuilder( PacketType.ARP );
					if ( _option.Port )
					{
						_ownPacketBuilderForScanPort = new OwnPacketBuilder( PacketType.TCP );
					}
					int i = 0;
					foreach ( int ipInt in _option.IpToTest.Result )
					{
						_ownPacketSender.trySend( _ownPacketBuilderForArping.BuildArpPacket( IPAddress.Parse( ( (uint)ipInt ).ToString() ).GetAddressBytes() ) );
						TaskCompleted( this, new TaskCompletedEventArgs( _option.OperationCount ) );
						if( _ownPacketSender._isIPV6 && _option.NbPacketToSend > 0 && _option.WaitTime > 0 )
						{
							i++;
							if( i == _option.NbPacketToSend )
							{
								i = 0;
								Thread.Sleep( _option.WaitTime );
							}
						}
					}
					AddOurNetworkInformation();
					_ownPacketReceiver.TimerToCallEndReceive();
				}
			} );
		}

		private void AddArpingInRepositoryAndContinueWithRequest( object sender, ArpingReceivedEventArgs e )
		{
			IPAddress datIP = IPAddress.Parse( e.IpAddress );
			if ( _option.CmdConsole )
			{
				Console.WriteLine( datIP );
			}
			//OwnPacketSender PacketSender = new OwnPacketSender( _option.NbPacketToSend, _option.WaitTime );
			Task.Factory.StartNew( () =>
					{
						if( _option.Arp )
						{
							_repository.AddOrUpdate( datIP, PhysicalAddress.Parse( e.MacAddress ) );
						}
					} ).ContinueWith( a =>
					{
						Task.Factory.StartNew( () =>
						{
							if( _option.Dns )
							{
								_repository.AddOrUpdate( datIP, _reverseDnsResolver.GetHostName( datIP ) );
							}
						});
						if( _option.Port )
						{
							int i = 0;
							foreach( ushort portNumber in _option.PortToTest.Result )
							{
								_ownPacketSender.trySend( _ownPacketBuilderForScanPort.BuildTcpPacket( e.IpAddress, e.MacAddress, portNumber ) );
								if ( _ownPacketSender._isIPV6 && _option.NbPacketToSend > 0 && _option.WaitTime > 0 )
								{
									i++;
									if ( i == _option.NbPacketToSend )
									{
										i = 0;
										Thread.Sleep( _option.WaitTime );
									}
								}
							}
						}
					} );
		}

		private void AddPortNumberInRepository( object sender, PortReceivedEventArgs e )
		{
			_repository.AddOrUpdate( IPAddress.Parse( e.IpAddress ), e.Port );
		}

		private void AddOurNetworkInformation()
		{
			IPAddress ownIpAddress;
			PhysicalAddress ownMacAddress;
			GetLocalInformation.LocalMacAndIPAddress( out ownIpAddress, out ownMacAddress );
			AddArpingInRepositoryAndContinueWithRequest( this, new ArpingReceivedEventArgs( ownIpAddress.ToString(), ownMacAddress.ToString() ) );
		}

		public int Progress()
		{
			int taskToDoPerIp = 8;
			//total of IP tested
			int IpTotal = 0;
			int restToDo = (IpTotal * taskToDoPerIp) / (IpTotal * taskToDoPerIp);
			return restToDo;
		}

		#endregion

		public void CloseAllThreads()
		{
			if ( _repository != null )
			{
				_repository.EndThreads();
			}
			_ownPacketReceiver.EndReceive();
		}

		public void SaveRepoXml( Stream stream )
		{
			_repository.XmlWriter( stream );
		}

		public void optionsModified( OptionUpdatedEventArgs e )
		{
			_option.Ping = e.Option.Ping;
			_option.Timeout = e.Option.Timeout;
			_option.Arping = e.Option.Arping;
			_option.Arp = e.Option.Arp;
			_option.Dns = e.Option.Dns;
			_option.Port = e.Option.Port;
			_option.PortToTestString = e.Option.PortToTestString;
			_option.MaximumTasks = e.Option.MaximumTasks;
			_option.NbPacketToSend = e.Option.NbPacketToSend;
			_option.WaitTime = e.Option.WaitTime;
			_option.TCPPort = e.Option.TCPPort;
			_option.UDPPort = e.Option.UDPPort;
			_option.RandomTCPPort = e.Option.RandomTCPPort;
			_option.RandomUDPPort = e.Option.RandomUDPPort;
		}
	}

	public class TaskCompletedEventArgs : EventArgs
	{

		public TaskCompletedEventArgs( int taskCompleted )
		{
			TaskCompleted = taskCompleted;
		}

		public int TaskCompleted { get; private set; }
	}
	public class PrintResultEventsArgs : EventArgs
	{
		public PrintResultEventsArgs()
		{

		}
	}
}
