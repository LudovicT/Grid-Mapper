﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Transport;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace GridMapper.NetworkUtilities
{
	public enum PacketType
	{
		Unknown,
		ARP,
		TCP,
	}

	public class OwnPacketReceiver
	{

		PacketDevice selectedDevice;
		string _filter = string.Empty;
		bool _isStart;
		Thread _threadForReceive;

		public event EventHandler<ArpingReceivedEventArgs> ArpingReceived;

		public OwnPacketReceiver( bool arp = true, bool tcp = true, ushort tcpPort = 62000, bool udp = false, ushort udpPort = 62001)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
			_isStart = true;

			if ( arp || tcp )
			{
				PhysicalAddress mac;
				Byte[] ip;
				GetLocalInformation.LocalMacAndIPAddress( out ip, out mac );
				string macPart1 = mac.ToString().Substring( 0, 4 ).ToUpper();
				string macPart2 = mac.ToString().Substring( 4, 4 ).ToUpper();
				string macPart3 = mac.ToString().Substring( 8, 4 ).ToUpper();
				if ( arp )
				{
					// arp is not comming from us
					_filter += "(arp && arp[8:2] != 0x" + macPart1 + " && arp[10:2] != 0x" + macPart2 + " && arp[12:2] != 0x" + macPart3 + ")";
					if ( tcp )
					{
						_filter += " || (tcp[tcpflags] & (tcp-ack) != 0 and tcp dst port " + tcpPort.ToString()
							+ " and not src " + new IPAddress( ip ).ToString() + ")";
					}
				}
				else if ( tcp )
				{
					_filter += "(tcp[tcpflags] & (tcp-ack) != 0 and tcp dst port " + tcpPort.ToString()
						+ " and not src " + new IPAddress( ip ).ToString() + ")";
				}
			}

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];
		}

		public void StartReceive()
		{
			_isStart = true;
			_threadForReceive = new Thread( Receive );
			_threadForReceive.Start();
		}

		private void Receive()
		{
			using( PacketCommunicator communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 ) )
			{
				Console.WriteLine( "Listening on " + selectedDevice.Description + "..." );
				// Retrieve the packets
				communicator.SetFilter( _filter );
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
								ArpingReceived( this, new ArpingReceivedEventArgs( packet.Ethernet.Arp.SenderProtocolIpV4Address.ToString(), ByteArrayToHexViaByteManipulation( packet.Ethernet.Arp.SenderHardwareAddress.ToArray() ) ) );
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
				} while( _isStart );
			}
		}

		public void EndReceive()
		{
			_isStart = false;
			_threadForReceive.Abort();
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

	public class ArpingReceivedEventArgs : EventArgs
	{
		public ArpingReceivedEventArgs( string ipAddress, string macAddress)
		{
			IpAddress = ipAddress;
			MacAddress = macAddress;
		}

		public string IpAddress { get; private set; }
		public string MacAddress { get; private set; }
	}
}
