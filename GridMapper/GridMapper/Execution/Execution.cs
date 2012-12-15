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

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;

		public delegate void TaskEndedEventHandler( object sender, TaskCompletedEventArgs e );
		public event TaskEndedEventHandler TaskCompleted;
		public event EventHandler IsFinished;

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

		public void StartScan()
		{
			int minWORK;
			int minIOC;
			int maxWORK;
			int maxIOC;
			ThreadPool.GetMinThreads( out minWORK, out minIOC );
			ThreadPool.GetMaxThreads( out maxWORK, out maxIOC );
			if( ThreadPool.SetMinThreads( 50, 50 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
			}

			Task task1 = Task.Factory.StartNew( () =>
				{
					PingSender pingSender = new PingSender( Option );
					ARPSender arpSender = new ARPSender();
					ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
					PortScanner portScanner = new PortScanner();
					Parallel.ForEach<int>( _option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
						{
							//PingSender pingSender = new PingSender( Option );
							IPAddress ip = IPAddress.Parse( ((uint)ipInt).ToString() );
							PingReply pingReply = pingSender.Ping( ip );
							if( pingReply != null )
							{
								_repository.AddOrUpdate( ip, pingReply );
								TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								if ( Option.Arp )
								{
									PhysicalAddress mac = arpSender.GetMac( ip );
									if ( mac != PhysicalAddress.None )
									{
										_repository.AddOrUpdate( ip, mac );
									}
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								}
								if ( Option.Dns )
								{
									IPHostEntry dns = dnsResolver.GetHostName( ip );
									if ( dns != null )
									{
										_repository.AddOrUpdate( ip, dns );
									}
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								}
								if ( Option.Port )
								{
									//ne gere pas le option des port a scan
									PortComputer portComputer = portScanner.ScanPort( ip, 80 );
									if ( portComputer.Port != 0 )
									{
										_repository.AddOrUpdate( ip, portComputer );
									}
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								}
							}
							else
								TaskCompleted( this, new TaskCompletedEventArgs( Option.OperationCount ) );
						} );
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

		public Execution( Option startupOptions )
		{
			_option = startupOptions;
			_repository = new Repository();
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
