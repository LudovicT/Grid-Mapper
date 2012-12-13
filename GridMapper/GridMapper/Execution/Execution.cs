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

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;


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
					Parallel.ForEach<int>( _option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
						{
							//PingSender pingSender = new PingSender( Option );
							IPAddress ip = IPAddress.Parse( ((uint)ipInt).ToString() );
							PingReply pingReply = pingSender.Ping( ip );
							if( pingReply != null )
							{
								_repository.AddOrUpdate( ip, pingReply );
								PhysicalAddress mac = arpSender.GetMac( ip );
								if ( mac != PhysicalAddress.None )
								{
									_repository.AddOrUpdate( ip, mac );
								}
								IPHostEntry dns = dnsResolver.GetHostName( ip );
								if ( dns != null )
								{
									_repository.AddOrUpdate( ip, dns );
								}
							}
						} );
				} );/*.ContinueWith( ( a ) =>
					{
						ARPSender arpSender = new ARPSender();
						ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
						Parallel.ForEach<IPAddress>( _repository.GetIPAddresses(), new ParallelOptions { MaxDegreeOfParallelism = 200 }, ip =>
						{
							_repository.AddOrUpdate( ip, arpSender.GetMac( ip ) );
						} );
						//Task task3 = Task.Factory.StartNew( () =>
						//{
						//    _repository.AddOrUpdate( ip, dnsResolver.GetHostName( ip ) );
						//} );
					} );
					/*} ).ContinueWith( ( b ) =>
						{
							//fait peter l'event
						} );*/
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
}
