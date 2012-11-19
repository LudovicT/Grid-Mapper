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
	public class TestUtilities
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
			PhysicalAddress fetchedMac = NetworkUtilities.GetMacAddress( TrueIp );

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
			NetworkUtilities.TaskGetMacAddress( Ips );
		}

        #endregion //ARPRegion

		#region PingRegion

		[Test]
		public void PerformanceTestAsyncPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "127.0.0.1" ) );
			}
			NetworkUtilities.ListPinger( addressToTest ,200);
		}

		[Test]
		public void PerformanceTestTaskPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "192.168.1.27" ) );
			}
			NetworkUtilities.TaskPinger( addressToTest ,200);
		}

		#endregion //PingRegion

		[Test]
		public void GetHostNameTest()
		{
			List<Task> tasks = new List<Task>();
			Task T1 = Task.Factory.StartNew( () =>
				{
					for( int i = 0 ; i < 400 ; i++ )
					{
						Console.WriteLine( i );
						tasks.Add( NetworkUtilities.GetHostName( IPAddress.Parse( "10.8.99.66" ) ) );
					}
				} );
			T1.Wait();
			Task.WaitAll( tasks.ToArray() );
			Console.WriteLine( "ok" );
        }

        #region PortScanRegion

        [Test]
        public void PerformTestScanPort()
        {
			List<Task> tasks = new List<Task>();
			Task T1 = Task.Factory.StartNew( () =>
			{
				for(int i = 0 ; i < 1024 ; i++ )
					tasks.Add( NetworkUtilities.ScanPort( IPAddress.Parse( "5.9.87.133" ), i) );
			} );
			T1.Wait();
			Task.WaitAll( tasks.ToArray() );
			Console.WriteLine( "ok" );
        }

        #endregion // PortScanRegion

		[Test]
		public void IpRange()
		{
			IPAddress ip = IPAddress.Parse("10.8.110.230");
			NetworkUtilities.IpRange(ip,20);
		}
		[Test]
		public void AutoIpRange()
		{
			NetworkUtilities.AutoIpRange().ForEach( IP => Console.WriteLine( IP ));
		}
    }
}
