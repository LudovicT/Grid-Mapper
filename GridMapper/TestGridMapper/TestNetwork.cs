using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using GridMapper.NetworkRawSender;
using System.Threading.Tasks;
using GridMapper.NetworkUtilities;

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
		public void GetInterfaces()
		{
			arp.interfaces();
		}
		[Test]
		public void Arp24()
		{
			arp wpcap = new arp();
			wpcap.tryARP( IPParser.TryParse( "192.168.1.0/24" ).Result );
		}
		[Test]
		public void Arp20()
		{
			arp wpcap = new arp();
			wpcap.tryARP( IPParser.TryParse( "192.168.1.0/20" ).Result );
		}
		[Test]
		public void Arp16()
		{
			arp wpcap = new arp();
			wpcap.tryARP( IPParser.TryParse( "192.168.1.0/16" ).Result );
		}
		[Test]
		public void Arp12()
		{
			arp wpcap = new arp();
			wpcap.tryARP( IPParser.TryParse( "192.168.1.0/12" ).Result );
		}
		[Test]
		public void Arp8()
		{
			arp wpcap = new arp();
			wpcap.tryARP( IPParser.TryParse( "192.168.1.0/8" ).Result );
		}
		[Test]
		public void ReceiveARP()
		{
			arp wpcap = new arp();
			wpcap.receiveARP();
		}
		[Test]
		public void TestScanPortWithConnect()
		{
			string ip = "192.168.1.27";
			for( int i = 1 ; i < 65535 ; i++ )
			{
				RawScanPort scanPort = new RawScanPort();
				scanPort.ScanWithConnect( IPAddress.Parse( ip ), i );
				//if( i % 20000 == 0 )
				//    Thread.Sleep( 300 );
			}
		}
		[Test]
		public void TestScanPortWithPcap()
		{
			string ip = "192.168.1.27";
			string mac = "00113202ce22";
			//PortScannerBis portScanner = new PortScannerBis();
			OwnPacketSender Sender = new OwnPacketSender();
			OwnPacketBuilder TCPPacket = new OwnPacketBuilder(PacketType.TCP);
			for( int i = 1 ; i <= 65535 ; i++ )
			{
				Sender.trySend( TCPPacket.BuildTcpPacket(ip,mac,(ushort)i) );
			}
		}
		[Test]
		public void StartReceive()
		{
			OwnPacketReceiver ownPacketReceiver = new OwnPacketReceiver();
			ownPacketReceiver.StartReceive();
		}

		//[Test]
		//public void TestScanPortWithTcpClient()
		//{
		//    IPAddress ip = IPAddress.Parse("192.168.1.27");
		//    ThreadPool.SetMinThreads( 200, 200 );
		//    for ( int i = 1; i < 65535; i++ )
		//    {
		//        TimeOutSocket.Connect( new IPEndPoint( ip, i ), 50 );
		//    }
		//}
	}
}
