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
using GridMapper.NetworkUtilities;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;

namespace GridMapper
{
	class NewExecution : IExecution, IPacketReceiverClient
	{
		Option _option;
		Repository _repository;
		NewPacketReceiver _ownPacketReceiver;
		OwnPacketBuilder _ownPacketBuilderForArping;
		OwnPacketBuilder _ownPacketBuilderForScanPort;
		OwnPacketSender _ownPacketSender;
		ReverseDnsResolver _reverseDnsResolver;
		Task _taskForExecution;

		public event EventHandler<TaskCompletedEventArgs> TaskCompleted;

		public NewExecution( Option startupOptions )
		{
            _option = startupOptions;

            _reverseDnsResolver = new ReverseDnsResolver();
		}

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

		public void StartScan()
		{
			_ownPacketReceiver = new NewPacketReceiver( _option.Arping, _option.Port, _option.TCPPort, _option.RandomUDPPort, _option.UDPPort, _option.Timeout );
			_ownPacketSender = new OwnPacketSender( _option.NbPacketToSend, _option.WaitTime );

			_repository = new Repository();
            _ownPacketReceiver.AttachClient( _repository );
			_ownPacketReceiver.AttachClient( this );
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
			//OwnPacketSender PacketSender = new OwnPacketSender( _option.NbPacketToSend, _option.WaitTime );
			if ( _option.CmdConsole )
			{
				Console.WriteLine( datIP + "	" + e.MacAddress + "	" + _reverseDnsResolver.GetHostName( datIP ).HostName );
			}
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
			if( _ownPacketReceiver != null )
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

		#region IPacketReceiverClient Members

		public void Update( PcapDotNet.Packets.Packet packet )
		{
			if( packet != null &&
				packet.Ethernet.EtherType == EthernetType.Arp &&
				packet.Ethernet.Arp.Operation == ArpOperation.Reply )
			{
				IPAddress datIP = IPAddress.Parse( packet.Ethernet.Arp.SenderProtocolIpV4Address.ToString() );
				//if( _option.CmdConsole )
				//{
				//	Console.WriteLine( datIP + "	" + GridWindow.ToMac( e.MacAddress ) + "	" + _reverseDnsResolver.GetHostName( datIP ).HostName );
				//}
				Task.Factory.StartNew( () =>
				{
					Task.Factory.StartNew( () =>
					{
						if( _option.Dns )
						{
							_repository.AddOrUpdate( datIP, _reverseDnsResolver.GetHostName( datIP ) );
						}
					} );
					if( _option.Port )
					{
						int i = 0;
						foreach( ushort portNumber in _option.PortToTest.Result )
						{
							_ownPacketSender.trySend( _ownPacketBuilderForScanPort.BuildTcpPacket( datIP.ToString(), NetworkUtility.ByteArrayToHexViaByteManipulation( packet.Ethernet.Arp.SenderHardwareAddress.ToArray() ), portNumber ) );
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
					}
				} );
			}
		}

		#endregion
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
