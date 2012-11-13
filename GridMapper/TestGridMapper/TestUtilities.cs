using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
using GridMapper;
using System.Net.NetworkInformation;

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
			PhysicalAddress fetchedMac = Utilities.GetMacAddress( TrueIp );

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
			Utilities.GlobalTaskGetMacAddress( Ips, 50 );
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
			Utilities.ListPinger( addressToTest );
		}

		[Test]
		public void PerformanceTestTaskPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "192.168.1.27" ) );
			}
			Utilities.TaskPinger( addressToTest );
		}

		#endregion //PingRegion

		[Test]
		public void GetHostNameTest()
		{
			IPHostEntry ipHost = Dns.GetHostEntry("10.8.102.131");
			Console.WriteLine(ipHost.HostName);
		}

	}
}
