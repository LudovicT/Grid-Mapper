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
			if( ThreadPool.SetMinThreads( 200, 200 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
			}

			Task task1 = Task.Factory.StartNew( () =>
				{
					PingSender pingSender = new PingSender( Option );
					ARPSender arpSender = new ARPSender();
					ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
					PortScanner portScanner = new PortScanner();

					RawSocketPing.SendPing += Repo;

					RawSocketPing pingSocket = null;
					string remoteHost = "localhost";
					IPAddress remoteAddress = null;
					int dataSize = 100, ttlValue = 128, sendCount = 1;
					IPHostEntry hostEntry = Dns.GetHostEntry( remoteHost );
					remoteAddress = hostEntry.AddressList[0];
					System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();

					pingSocket = new RawSocketPing( remoteAddress.AddressFamily, ttlValue, dataSize, sendCount, (ushort)proc.Id );

					//Parallel.ForEach<int>( _option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
					//    {
					foreach(int ipInt in _option.IpToTest.Result)
					{
						Task.Factory.StartNew( () =>
							{
								//PingSender pingSender = new PingSender( Option );
								IPAddress ip = IPAddress.Parse( ((uint)ipInt).ToString() );

								pingSocket.PingAddress = ip;
								//pingSocket.InitializeSocket();
								//pingSocket.BuildPingPacket();
								//pingSocket.DoPing();
								RawSocketPing.SendPingStatic( ip );
							} );

							//PingReply pingReply = pingSender.Ping( ip );
							//if( pingReply != null )
							//{
							//    _repository.AddOrUpdate( ip, pingReply );
							//    TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
							//    if ( Option.Arp )
							//    {
							//        PhysicalAddress mac = arpSender.GetMac( ip );
							//        if ( mac != PhysicalAddress.None )
							//        {
							//            _repository.AddOrUpdate( ip, mac );
							//        }
							//        TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
							//    }
							//    if ( Option.Dns )
							//    {
							//        IPHostEntry dns = dnsResolver.GetHostName( ip );
							//        if ( dns != null )
							//        {
							//            _repository.AddOrUpdate( ip, dns );
							//        }
							//        TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
							//    }
							//    if ( Option.Port )
							//    {
							//        //ne gere pas l'option des port a scan
							//        ushort portToTest = 80;
							//        if ( portScanner.ScanPort( ip, portToTest ) )
							//        {
							//            _repository.AddOrUpdate( ip, portToTest );
							//        }
							//        TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
							//    }
							//}
							//else
							//    TaskCompleted( this, new TaskCompletedEventArgs( Option.OperationCount ) );
						//} );
				}
				} ).ContinueWith( (a) =>
					{
						_repository.EndThreads();
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
	}

	public class TaskCompletedEventArgs : EventArgs
	{

		public TaskCompletedEventArgs( int taskCompleted )
		{
			TaskCompleted = taskCompleted;
		}

		public int TaskCompleted { get; private set; }
	}
}
