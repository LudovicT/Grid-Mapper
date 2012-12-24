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

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;
		
		public event EventHandler<TaskCompletedEventArgs> TaskCompleted;

		#region IExecution Membres

		public Option Option { get { return _option; } }

		public IRepository Repository { get { return _repository; } }

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
					Parallel.ForEach<int>( Option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
						{
							IPAddress ip = IPAddress.Parse( ((uint)ipInt).ToString() );
							PingReply pingReply = null;
							PhysicalAddress mac = null;
							if ( Option.Ping )
							{
								pingReply = pingSender.Ping( ip );
							}
							if ( Option.Arping )
							{
								Task<PhysicalAddress> task = Task<PhysicalAddress>.Factory.StartNew( () =>
								arpSender.GetMac( ip ) );
								mac = task.Result;
							}
							if ( pingReply != null || ( mac != null && mac != PhysicalAddress.None ) )
							{
								if ( pingReply != null )
								{
									_repository.AddOrUpdate( ip, pingReply );
								}
								TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );

								if ( pingReply != null )
								{
									Task<PhysicalAddress> task = Task<PhysicalAddress>.Factory.StartNew(() =>
									arpSender.GetMac( ip ));
									mac = task.Result;
								}
								if ( mac != null && mac != PhysicalAddress.None)
								{
									_repository.AddOrUpdate( ip, mac );
								}
								if ( Option.Arp )
								{
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								}

								if ( Option.Dns )
								{
									IPHostEntry dns = dnsResolver.GetHostName( ip );
									if ( dns != null )
									{
										_repository.AddOrUpdate( ip, dns );
									}
									else
									{
										dns = new IPHostEntry(){HostName = ip.ToString()};
										_repository.AddOrUpdate( ip, dns );
									}
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
								}

								if ( Option.Port )
								{
									foreach ( ushort portToTest in Option.PortToTest.Result )
									{
										if ( portScanner.ScanPort( ip, portToTest ) )
										{
											_repository.AddOrUpdate( ip, portToTest );
										}
									}
									TaskCompleted( this, new TaskCompletedEventArgs( 1 ) );
									
								}
								//TaskCompleted( this, new Data());
							}
							else
							{
								TaskCompleted( this, new TaskCompletedEventArgs( Option.OperationCount ) );
							}
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
			_option.Arping = e.Arping;
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
