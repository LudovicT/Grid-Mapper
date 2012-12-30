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
		EthernetLayer ethernetLayer;
		IpV4Layer ipV4Layer;
		TcpLayer tcpLayer;
		ArpLayer arpLayer;

		public OwnPacketBuilder(PacketType packetType)
		{
			GetLocalInformation.LocalMacAndIPAddress( out _ownIpAddress, out _ownMacAddress );
			ethernetLayer = null;
			ipV4Layer = null;
			tcpLayer = null;
			arpLayer = null;
			switch(packetType)
			{
				case PacketType.ARP :
					BuildingArpPacket();
					break;
				case PacketType.TCP :
					BuildingTcpPacket();
					break;
				case PacketType.Unknown :
					break;
				default :
					break;
			}
		}

		public Packet BuildTcpPacket( string ipAddress, string macAddress, int portToScan )
		{
			ethernetLayer.Destination = new MacAddress( ToMac( macAddress.ToString() ) );
			ipV4Layer.CurrentDestination = new IpV4Address( ipAddress );
			tcpLayer.DestinationPort = (ushort)portToScan;

			PacketBuilder builder = new PacketBuilder( ethernetLayer, ipV4Layer, tcpLayer );

			return builder.Build( DateTime.Now );
		}

		void BuildingTcpPacket()
		{
			ethernetLayer = new EthernetLayer
			{
				Source = new MacAddress( ToMac( _ownMacAddress.ToString() ) ),
				EtherType = EthernetType.None, // Will be filled automatically.
			};
			ipV4Layer = new IpV4Layer
			{
				Source = new IpV4Address( new IPAddress( _ownIpAddress ).ToString() ),
				Fragmentation = IpV4Fragmentation.None,
				HeaderChecksum = null, // Will be filled automatically.
				Identification = 0,
				Options = IpV4Options.None,
				Protocol = null, // Will be filled automatically.
				Ttl = 100,
				TypeOfService = 0,
			};
			tcpLayer = new TcpLayer
			{
				SourcePort = 62000,
				Checksum = null, // Will be filled automatically.
				SequenceNumber = 100,
				AcknowledgmentNumber = 0,
				ControlBits = TcpControlBits.Synchronize,
				Window = 8192,
				UrgentPointer = 0,
				Options = new TcpOptions( new TcpOptionMaximumSegmentSize( 1460 ), TcpOption.Nop, new TcpOptionWindowScale( 2 ), new TcpOptionSelectiveAcknowledgmentPermitted(), new TcpOptionTimestamp( (uint)DateTime.Now.Ticks, 0 ) ),
			};

		}

		/// <summary>
		/// This function build an ARP over Ethernet packet.
		/// </summary>
		public Packet BuildArpPacket( byte[] destIP )
		{
			if ( ethernetLayer == null && arpLayer == null )
			{
				throw new InvalidOperationException( "Trying to build an ARP packet while BuildingArpPacket() wasn't invoked first." );
			}
			arpLayer.TargetProtocolAddress = Array.AsReadOnly( destIP );

			PacketBuilder builder = new PacketBuilder( ethernetLayer, arpLayer );

			return builder.Build( DateTime.Now );			
		}

		void BuildingArpPacket()
		{
			ethernetLayer = new EthernetLayer
				{
					Source = new MacAddress( ToMac( _ownMacAddress.ToString() ) ),
					Destination = new MacAddress( "FF:FF:FF:FF:FF:FF" ),
					EtherType = EthernetType.None, // Will be filled automatically.
				};
			arpLayer = new ArpLayer
				{
					ProtocolType = EthernetType.IpV4,
					Operation = ArpOperation.Request,
					SenderHardwareAddress = Array.AsReadOnly( _ownMacAddress.GetAddressBytes().ToArray() ), // 03:03:03:03:03:03.
					SenderProtocolAddress = Array.AsReadOnly( _ownIpAddress ), // 1.2.3.4.
					TargetHardwareAddress = Array.AsReadOnly( new byte[] { 0, 0, 0, 0, 0, 0 } ), // 00:00:00:00:00:00.
				};

		}

		static string ToMac( string ToTransform )
		{
			ToTransform = ToTransform.ToUpperInvariant();
			var list = Enumerable
				.Range( 0, ToTransform.Length / 2 )
				.Select( i => ToTransform.Substring( i * 2, 2 ) );
			return string.Join( ":", list );
		}
	}
}
