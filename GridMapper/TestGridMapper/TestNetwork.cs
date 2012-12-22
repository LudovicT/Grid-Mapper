using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using System.Net;
using GridMapper.NetworkRawSender;
using System.Threading;
using System.Reflection;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestNetwork
	{
	//    [Test]
	//    public void TestPingCollectionAdd()
	//    {
	//        foreach ( IPAddress ip in IPRange.AutoIpRange() )
	//        {
	//            new PingSender().Ping( ip, 120 );
	//        }
	//        Thread.Sleep( 5000 );
	//        Assert.That( Network.IPAddresses.Count >= 1 );
	//        Network.IPAddresses.Clear();
	//    }

	//    [Test]
	//    public void TestARPCollectionAdd()
	//    {
	//        foreach ( IPAddress ip in IPRange.AutoIpRange() )
	//        {
	//            new ARPSender().GetMac( ip);
	//        }
	//        Thread.Sleep( 5000 );
	//        Assert.That( Network.MacAddresses.Count >= 1 );
	//        Network.MacAddresses.Clear();
	//    }

	//    [Test]
	//    public void TestHostNameCollectionAdd()
	//    {
	//        foreach ( IPAddress ip in IPRange.AutoIpRange() )
	//        {
	//            new ReverseDnsResolver().GetHostName( ip );
	//        }
	//        Thread.Sleep( 5000 );
	//        Assert.That( Network.HostsEntries.Count >= 1 );
	//        Network.HostsEntries.Clear();
	//    }

		[Test]
		public void TestScanPortWithConnect()
		{
			string ip = "10.8.110.230";
			for( int i = 1 ; i < 65535 ; i++ )
			{
				RawScanPort scanPort = new RawScanPort();
				scanPort.ScanWithConnect( IPAddress.Parse( ip ), i );
			}
			Thread.Sleep( 10000 );
		}
	}
}
