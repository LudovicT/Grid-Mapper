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
using System.Threading.Tasks;
using GridMapper.NetworkRawSender;
using GridMapper.NetworkUtilities;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestNetwork
	{
		[Test]
		public void DeleteARPCache()
		{
			Assert.That( Arping.DeleteCachedARP() == true, "ARP not deleted" );
		}
		[Test]
		public void RetreiveARPCache()
		{
			Assert.That( Arping.GetARPaResult() !=string.Empty );
			Console.WriteLine( Arping.GetARPaResult() );
		}
		[Test]
		public void DeletePingAndRetreiveARPCache()
		{
			Console.WriteLine( "Before Deletion:" );
			Console.WriteLine( Arping.GetARPaResult() );

			Assert.That( Arping.DeleteCachedARP() == true, "ARP not deleted" );

			Console.WriteLine( "After Deletion:" );
			Console.WriteLine( Arping.GetARPaResult() );

			Option Option = new Option();
			Option.IpToTest = IPRange.AutoIpRange();
			PingSender pingSender = new PingSender( Option );
			ThreadPool.SetMinThreads( 200, 200 );
			Parallel.ForEach<int>( Option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
			{
				IPAddress ip = IPAddress.Parse( ( (uint)ipInt ).ToString() );
				pingSender.AsyncPing( ip );
			} );
			Console.WriteLine( "After Ping" );
			Console.WriteLine( Arping.GetARPaResult() );
		}

		[Test]
		public void ARPCacheTryParse()
		{
			foreach ( var arp in Arping.GetARPaResult().Split( new char[] { '\n', '\r' } ) )
			{
				// Parse out all the MAC / IP Address combinations
				if ( !string.IsNullOrEmpty( arp ) )
				{
					var pieces = ( from piece in arp.Split( new char[] { ' ', '\t' } )
								   where !string.IsNullOrEmpty( piece )
								   select piece ).ToArray();
					if ( pieces.Length == 3  && pieces[2] == "dynamique")
					{
						Console.WriteLine( pieces[0] + " " + pieces[1] + " " + pieces[2]);
					}
				}
			}
		}

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

		[Test]
		public void TestScanRouter()
		{
			string ipTarget = "192.168.1.27";
			string macRouter = "c8cd723a8708";
			//PortScannerBis portScanner = new PortScannerBis();
			OwnPacketSender Sender = new OwnPacketSender();
			OwnPacketBuilder ICMPPacket = new OwnPacketBuilder( PacketType.TCP );
			for( int i = 1 ; i <= 10 ; i++ )
			{
				Sender.trySend( ICMPPacket.BuildIcmpPacket(ipTarget, macRouter) );
			}
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
