using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;

namespace GridMapper.NetworkUtilities
{
	public class OwnPacketBuilder
	{
		byte[] _ownIpAddress;
		PhysicalAddress _ownMacAddress;
		ushort _portToScan

		public OwnPacketBuilder()
		{
			getLocalMacAndIP( out _ownIpAddress, out _ownMacAddress );
		}

		public Packet BuildTcpPacket( string ipAddress, string macAddress, int portToScan )
		{
			EthernetLayer ethernetLayer = new EthernetLayer
			{
				Source = new MacAddress( ToMac( _ownMacAddress.ToString() ) ),
				Destination = new MacAddress( ToMac( macAddress.ToString() ) ),
				EtherType = EthernetType.None, // Will be filled automatically.
			};

			IpV4Layer ipV4Layer = new IpV4Layer
			{
				Source = new IpV4Address( new IPAddress( _ownIpAddress ).ToString() ),
				CurrentDestination = new IpV4Address( ipAddress ),
				Fragmentation = IpV4Fragmentation.None,
				HeaderChecksum = null, // Will be filled automatically.
				Identification = 0,
				Options = IpV4Options.None,
				Protocol = null, // Will be filled automatically.
				Ttl = 100,
				TypeOfService = 0,
			};

			TcpLayer tcpLayer = new TcpLayer
			{
				SourcePort = 62000,
				DestinationPort = (ushort)portToScan,
				Checksum = null, // Will be filled automatically.
				SequenceNumber = 100,
				AcknowledgmentNumber = 0,
				ControlBits = TcpControlBits.Synchronize,
				Window = 8192,
				UrgentPointer = 0,
				Options = new TcpOptions( new TcpOptionMaximumSegmentSize( 1460 ), TcpOption.Nop, new TcpOptionWindowScale( 2 ), new TcpOptionSelectiveAcknowledgmentPermitted(), new TcpOptionTimestamp( (uint)DateTime.Now.Ticks, 0 ) ),
			};
			PayloadLayer payloadLayer = new PayloadLayer
			{
				Data = new Datagram( Encoding.ASCII.GetBytes( "test" ) ),
			};



			PacketBuilder builder = new PacketBuilder( ethernetLayer, ipV4Layer, tcpLayer, payloadLayer );

			return builder.Build( DateTime.Now );
		}

		/// <summary>
		/// This function build an ARP over Ethernet packet.
		/// </summary>
		public Packet BuildArpPacket( byte[] destIP )
		{
			EthernetLayer ethernetLayer = new EthernetLayer
				{
					Source = new MacAddress( ToMac( _ownMacAddress.ToString() ) ),
					Destination = new MacAddress( "FF:FF:FF:FF:FF:FF" ),
					EtherType = EthernetType.None, // Will be filled automatically.
				};
			ArpLayer arpLayer = new ArpLayer
				{
					ProtocolType = EthernetType.IpV4,
					Operation = ArpOperation.Request,
					SenderHardwareAddress = Array.AsReadOnly( _ownMacAddress.GetAddressBytes().ToArray() ), // 03:03:03:03:03:03.
					SenderProtocolAddress = Array.AsReadOnly( _ownIpAddress ), // 1.2.3.4.
					TargetHardwareAddress = Array.AsReadOnly( new byte[] { 0, 0, 0, 0, 0, 0 } ), // 00:00:00:00:00:00.
				};

			arpLayer.TargetProtocolAddress = Array.AsReadOnly( destIP );

			PacketBuilder builder = new PacketBuilder( ethernetLayer, arpLayer );

			return builder.Build( DateTime.Now );
		}

		static string ToMac( string ToTransform )
		{
			ToTransform = ToTransform.ToUpperInvariant();
			var list = Enumerable
				.Range( 0, ToTransform.Length / 2 )
				.Select( i => ToTransform.Substring( i * 2, 2 ) );
			return string.Join( ":", list );
		}

		static void getLocalMacAndIP( out byte[] ip, out PhysicalAddress mac )
		{
			ip = new byte[4];
			mac = PhysicalAddress.None;
			//get the interface
			NetworkInterface TrueNic = null;
			foreach( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
			{
				if( nic.OperationalStatus == OperationalStatus.Up )
				{
					TrueNic = nic;
					break;
				}
			}

			//get the interface ipv4
			foreach( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					ip = ips.GetAddressBytes();
					break;
				}
			}
			//get the interface mac
			mac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() );
		}
	}
}
