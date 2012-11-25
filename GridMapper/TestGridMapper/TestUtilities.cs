using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
using GridMapper;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Threading;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestARP
	{
		#region ARPRegion

		[Test]
		public void macTest()
		{
			//get the interface
			NetworkInterface TrueNic = null;
			foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
			{
				if ( nic.OperationalStatus == OperationalStatus.Up )
				{
					TrueNic = nic;
					break;
				}
			}
			Assert.That( TrueNic != null , "No valid interface to test with");

			//get the interface ipv4
			IPAddress TrueIp = null;
			foreach ( IPAddress ips in System.Net.Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					TrueIp = ips;
					break;
				}
			}
			//get the interface mac
			PhysicalAddress TrueMac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() );
			//get the interface mac from arp
			PhysicalAddress fetchedMac = new ARPSender().GetMac( TrueIp );

			Assert.That( fetchedMac.ToString()  == TrueMac.ToString() , "physical addresses does not match");
		}
		[Test]
		public void taskMacTest()
		{
			//get the interface
			NetworkInterface TrueNic = null;
			foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
			{
				if ( nic.OperationalStatus == OperationalStatus.Up )
				{
					TrueNic = nic;
					break;
				}
			}
			Assert.That( TrueNic != null, "No valid interface to test with" );

			//get the interface ipv4
			IPAddress TrueIp = null;
			foreach ( IPAddress ips in System.Net.Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					TrueIp = ips;
					break;
				}
			}
			//get the interface mac
			PhysicalAddress TrueMac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() );

			//get the interface mac from arp
			List<IPAddress> Ips = new List<IPAddress>();
			for ( int i = 0; i < 4000; i++ )
			{
				//Ips.Add( TrueIp );
				Ips.Add( TrueIp );
			}
			foreach(IPAddress Ip in Ips)
			{
				new ARPSender().GetMac( Ip );
			}
		}

        #endregion //ARPRegion
	}
	[TestFixture]
	public class TestPING
	{
		#region PingRegion

		[Test]
		public void PerformanceTestPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "8.8.8.8" ) );
			}
			foreach ( IPAddress Ip in addressToTest )
			{
				Console.WriteLine(new PingSender().Ping( Ip , 200 ).Status);
			}
		}


		#endregion //PingRegion
    }
	[TestFixture]
	public class TestDNSSolver
	{
		#region DNSRegion
		[Test]
		public void GetHostNameTest()
		{
			for( int i = 0 ; i < 400 ; i++ )
			{
				Console.WriteLine( i );
				new ReverseDnsResolver().GetHostName( IPAddress.Parse( "8.8.8.8" ) );
			}
			Console.WriteLine( "ok" );
		}
		#endregion //DNSRegion
	}
	[TestFixture]
	public class TestPortScan
	{
        #region PortScanRegion

        [Test]
        public void PerformTestScanPortWithParallelForeach()
		{
			int minWORK;
			int minIOC;
			int maxWORK;
			int maxIOC;
			ThreadPool.GetMinThreads( out minWORK, out minIOC );
			ThreadPool.GetMaxThreads( out maxWORK, out maxIOC );
			Console.WriteLine( "min : " + minWORK + " " + minIOC );
			Console.WriteLine( "max : " + maxWORK + " " + maxIOC );
			if ( ThreadPool.SetMinThreads( 50, 50 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
				Console.WriteLine( "min : " + minWORK + " " + minIOC );
			}
			List<int> inc = new List<int>();
			for ( int i = 0; i < 1024; i++ )
			{
				inc.Add(i);
			}
			Parallel.ForEach<int>(inc,new ParallelOptions { MaxDegreeOfParallelism = 200 }, i => new PortScanner().ScanPort( IPAddress.Parse( "127.0.0.1" ), i));
			Console.WriteLine( "ok" );
        }

		[Test]
		public void PerformTestScanPortWithTasks()
		{
			int minWORK;
			int minIOC;
			int maxWORK;
			int maxIOC;
			ThreadPool.GetMinThreads( out minWORK, out minIOC );
			ThreadPool.GetMaxThreads( out maxWORK, out maxIOC );
			Console.WriteLine( "min : " + minWORK + " " + minIOC );
			Console.WriteLine( "max : " + maxWORK + " " + maxIOC );
			if ( ThreadPool.SetMinThreads( 50, 50 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
				Console.WriteLine( "min : " + minWORK + " " + minIOC );
			}
			const int taskCount = 1024;
			List<Task> tasks = new List<Task>();
			for ( int i = 0; i < taskCount; i++ )
			{
				int temp = i;
				tasks.Add( Task.Factory.StartNew( () => new PortScanner().ScanPort( IPAddress.Parse( "127.0.0.1" ), temp ) ) );
			}
			Task.WaitAll( tasks.ToArray() );
		}

		[Test]
		public void PerformTestScanPortWithPLINQ()
		{
			const int taskCount = 1024;
			int minWORK;
			int minIOC;
			int maxWORK;
			int maxIOC;
			ThreadPool.GetMinThreads( out minWORK, out minIOC );
			ThreadPool.GetMaxThreads( out maxWORK, out maxIOC );
			Console.WriteLine( "min : " + minWORK + " " + minIOC );
			Console.WriteLine( "max : " + maxWORK + " " + maxIOC );
			if ( ThreadPool.SetMinThreads( 50, 50 ) )
			{
				ThreadPool.GetMinThreads( out minWORK, out minIOC );
				Console.WriteLine( "min : " + minWORK + " " + minIOC );
			}
			List<Action> tasks = new List<Action>();
			for ( int i = 0; i < taskCount; i++ )
			{
				int temp = i;
				tasks.Add( () => new PortScanner().ScanPort( IPAddress.Parse( "127.0.0.1" ), temp ) );
			}
			Task task = Task.Factory.StartNew( () => tasks.AsParallel().WithDegreeOfParallelism( 50 ).ForAll(t => t.Invoke()) );
			task.Wait();
		}
        #endregion // PortScanRegion
		
    }
	[TestFixture]
	public class TestIpRange
	{
		#region IPRange
		[Test]
		public void IpRange()
		{
			IPAddress ip = IPAddress.Parse("10.8.110.230");
			IPRange.IpRange(ip,20);
		}
		[Test]
		public void AutoIpRange()
		{
			IPRange.AutoIpRange().ForEach( IP => Console.WriteLine( IP ));
		}
		#endregion //IPRange
    }
}
