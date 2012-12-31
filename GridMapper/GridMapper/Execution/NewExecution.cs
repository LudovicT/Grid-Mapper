﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkRepository;
using System.Threading;
using System.Threading.Tasks;
using GridMapper.NetworkRawSender;
using GridMapper.NetworkUtilities;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

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

		public event EventHandler<TaskCompletedEventArgs> TaskCompleted;
		public event EventHandler IsFinished;

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

		private void SetThread()
		{
			int minWORK;
			int minIOC;
			int maxWORK;
			int maxIOC;
			ThreadPool.GetMinThreads( out minWORK, out minIOC );
			ThreadPool.GetMaxThreads( out maxWORK, out maxIOC );
			if( ThreadPool.SetMinThreads( 4, 4 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
			}
		}

		public void StartScan()
		{
			SetThread();
			_repository = new Repository();
			_ownPacketReceiver.StartReceive();

			Task task1 = Task.Factory.StartNew( () =>
			{
				_ownPacketBuilderForArping = new OwnPacketBuilder( PacketType.ARP );
				_ownPacketBuilderForScanPort = new OwnPacketBuilder( PacketType.TCP );
				//arp wpcap = new arp();
				//wpcap.tryARP( _option.IpToTest.Result );
				foreach( int ipInt in _option.IpToTest.Result )
				{
					_ownPacketSender.trySend( _ownPacketBuilderForArping.BuildArpPacket( IPAddress.Parse( ((uint)ipInt).ToString() ).GetAddressBytes() ) );
				}
				Thread.Sleep( 30000 );
				_ownPacketReceiver.EndReceive();
			} );
		}

		private void AddArpingInRepositoryAndContinueWithRequest( object sender, ArpingReceivedEventArgs e )
		{
			IPAddress datIP = IPAddress.Parse( e.IpAddress );
			_repository.AddOrUpdate( datIP, PhysicalAddress.Parse( e.MacAddress ) );
			_repository.AddOrUpdate( datIP, _reverseDnsResolver.GetHostName( datIP ) );
			if( _option.Port )
				foreach( ushort portNumber in _option.PortToTest )
					_ownPacketSender.trySend( _ownPacketBuilderForScanPort.BuildTcpPacket( e.IpAddress, e.MacAddress, portNumber ) );
		}

		public int Progress()
		{
			int taskToDoPerIp = 3;
			//total of IP tested
			int IpTotal = 0;
			int restToDo = (IpTotal * taskToDoPerIp) / (IpTotal * taskToDoPerIp);
			return restToDo;
		}
		#endregion

		public void SaveRepoXml( Stream stream )
		{
			_repository.XmlWriter( stream );
		}

		public NewExecution( Option startupOptions )
		{
			_option = startupOptions;
			_ownPacketReceiver = new OwnPacketReceiver();
			_ownPacketSender = new OwnPacketSender();
			_reverseDnsResolver = new ReverseDnsResolver();

			_ownPacketReceiver.ArpingReceived += AddArpingInRepositoryAndContinueWithRequest;
		}

		public void optionsModified( OptionUpdatedEventArgs e )
		{
			_option.Arp = e.Arp;
			_option.Dns = e.Dns;
			_option.Port = e.Port;
			_option.PingTimeout = e.Timeout;
			_option.MaximumTasks = e.Tasks;
			_option.PortToTest = e.Ports;
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
