using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Transport;

namespace GridMapper.NetworkUtilities
{
	public class OwnPacketReceiver
	{

		PacketDevice selectedDevice;

		public OwnPacketReceiver()
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];
		}

		public void StartReceive()
		{
			using( PacketCommunicator communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 ) )
			{
				Console.WriteLine( "Listening on " + selectedDevice.Description + "..." );
				// Retrieve the packets
				int nbPackets;
				//communicator.ReceivePackets(0, PacketHandler );

				Packet packet;
				do
				{
					PacketCommunicatorReceiveResult result = communicator.ReceivePacket( out packet );
					if( packet != null && 
						packet.Ethernet.EtherType == EthernetType.Arp && 
						packet.Ethernet.Arp.Operation == ArpOperation.Reply )
					{
						switch( result )
						{
							case PacketCommunicatorReceiveResult.Timeout:
								// Timeout elapsed
								continue;
							case PacketCommunicatorReceiveResult.Ok:
								Console.Write( "ip = " + packet.Ethernet.Arp.SenderProtocolIpV4Address.ToString() + " mac = " + ByteArrayToHexViaByteManipulation( packet.Ethernet.Arp.SenderHardwareAddress.ToArray() ) );
								Console.WriteLine();
								break;
							default:
								throw new InvalidOperationException( "The result " + result + " should never be reached here" );
						}
					}
					if( packet != null &&
						packet.Ethernet.IpV4 != null &&
						packet.Ethernet.IpV4.Tcp != null &&
						packet.Ethernet.IpV4.Tcp.IsValid &&
						packet.Ethernet.IpV4.Tcp.IsSynchronize &&
						packet.Ethernet.IpV4.Tcp.IsAcknowledgment &&
						packet.Ethernet.IpV4.Tcp.DestinationPort == 62000 )
						{
							switch( result )
							{
								case PacketCommunicatorReceiveResult.Timeout:
									// Timeout elapsed
									continue;
								case PacketCommunicatorReceiveResult.Ok:
									Console.Write( "ip = " + packet.Ethernet.IpV4.Source.ToString() + " Port = " + packet.Ethernet.IpV4.Tcp.SourcePort.ToString() + " portDest : " + packet.Ethernet.IpV4.Tcp.DestinationPort.ToString() );
									Console.WriteLine();
									break;
								default:
									throw new InvalidOperationException( "The result " + result + " should never be reached here" );
							}
						}
				} while( true );
			}
		}

		private string ByteArrayToHexViaByteManipulation( byte[] bytes )
		{
			char[] c = new char[bytes.Length * 2];
			byte b;
			for( int i = 0 ; i < bytes.Length ; i++ )
			{
				b = ((byte)(bytes[i] >> 4));
				c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(bytes[i] & 0xF));
				c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			}
			return new string( c );
		}
	}
}
