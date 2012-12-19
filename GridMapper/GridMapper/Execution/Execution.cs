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
using ConsoleApplication5;
using GridMapper.NetworkRawSender;

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;
		
		public event EventHandler<TaskCompletedEventArgs> TaskCompleted;
		public event EventHandler IsFinished;

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

		public void Repo( object sender, SendPingEventArgs e )
		{
			_repository.AddOrUpdate( e.IpAddress );
		}

		public void StartScan()
		{
			_repository = new Repository();
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

			Task task1 = Task.Factory.StartNew( () =>
				{
					PingSender pingSender = new PingSender( Option );
					ARPSender arpSender = new ARPSender();
					ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
					PortScanner portScanner = new PortScanner();

					SendRawPing.SendPing += Repo;

					SendRawPing pingSocket = new SendRawPing();

					//Parallel.ForEach<int>( _option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
					//    {
							foreach(int ipInt in _option.IpToTest.Result)
							{
							IPAddress ip = IPAddress.Parse( ( (uint)ipInt ).ToString() );
							pingSocket.Send( ip );


							//PingReply pingReply = null;
							//PhysicalAddress mac = null;
							//if ( Option.Ping )
							//{
							//    pingReply = pingSender.Ping( ip );
							//}
							//if ( Option.Arp )
							//{
							//    Task<PhysicalAddress> task = Task<PhysicalAddress>.Factory.StartNew( () =>
							//    arpSender.GetMac( ip ) );
							//    mac = task.Result;
							//}
							//if ( pingReply != null || ( mac != null && mac != PhysicalAddress.None ) )
							//{
							//    if ( pingReply != null )
							//    {
							//        _repository.AddOrUpdate( ip, pingReply );
							//        Task<PhysicalAddress> task = Task<PhysicalAddress>.Factory.StartNew(() =>
							//        arpSender.GetMac( ip ));
							//        mac = task.Result;
							//    }
							//    if ( mac != null && mac != PhysicalAddress.None)
							//    {
							//        _repository.AddOrUpdate( ip, mac );
							//    }
							//    if ( Option.Arp )
							//    {
							//{
							//}
						}
				} ).ContinueWith( (a) =>
					{
						//_repository.EndThreads();
						//IsFinished( this, null );
						//TaskCompleted( this, null );
					} );
		}

        public int Progress()
        {
            int taskToDoPerIp = 3;
            //total of IP tested
            int IpTotal = 0;
           int restToDo = ( IpTotal * taskToDoPerIp ) / ( IpTotal * taskToDoPerIp );
            return restToDo;
        }
		#endregion

		public void SaveRepoXml(Stream stream)
		{
			_repository.XmlWriter(stream);
		}

		public Execution( Option startupOptions )
		{
			_option = startupOptions;
		}

		public void optionsModified(OptionUpdatedEventArgs e)
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
