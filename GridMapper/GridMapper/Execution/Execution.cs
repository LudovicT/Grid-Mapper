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
			Task task1 = Task.Factory.StartNew( () =>
				{
					//PingSender pingSender = new PingSender( Option );
					Parallel.ForEach<int>( _option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
						{
							PingSender pingSender = new PingSender( Option );
							IPAddress ip = IPAddress.Parse( ipInt.ToString() );
							_repository.AddOrUpdate( ip, pingSender.Ping( ip ) );
						} );
				} ); // a retirer quand on enelve les comms
				/*} ).ContinueWith( ( a ) =>
					{
						ARPSender arpSender = new ARPSender();
						ReverseDnsResolver dnsResolver = new ReverseDnsResolver();
						foreach( IPAddress ip in _repository.GetIPAddresses() )
						{
							Task task2 = Task.Factory.StartNew( () =>
							{
								_repository.AddOrUpdate( ip, arpSender.GetMac( ip ) );
							} );
							Task task3 = Task.Factory.StartNew( () =>
							{
								_repository.AddOrUpdate( ip, dnsResolver.GetHostName( ip ) );
							} );
						}
					} ).ContinueWith( ( b ) =>
						{
							//fait peter l'event
						} );*/
		}

		#endregion

		public Execution( Option startupOptions )
		{
			_option = startupOptions;
			_repository = new Repository();
		}
	}
}
