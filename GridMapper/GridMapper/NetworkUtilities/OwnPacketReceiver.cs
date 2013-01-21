using System;
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
		ushort _tcpPort;
		bool _isStart;
		bool _isActive;
		Thread _threadForReceive;
		System.Timers.Timer timer;
		readonly bool _isIPV6 = false;

		public event EventHandler<ArpingReceivedEventArgs> ArpingReceived;
		public event EventHandler<PortReceivedEventArgs> PortReceived;
		public static event EventHandler EndOfScan;

		public OwnPacketReceiver( bool arp = true, bool tcp = true, ushort tcpPort = 62000, bool udp = false, ushort udpPort = 62001)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
			_isStart = true;
			_isActive = true;
			_tcpPort = tcpPort;

			timer = new System.Timers.Timer();
			timer.Interval = 5000;
			timer.Enabled = true;
			timer.Elapsed += EndReceiveCallByTimer;
			timer.Stop();

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}

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

			for ( int i = 0; i < allDevices.Count; i++ )
			{
				for ( int j = 0; j < allDevices[i].Addresses.Count; j++ )
				{
					string[] deviceAddress = allDevices[i].Addresses[j].Address.ToString().Split( ' ' );
					if ( allDevices[i].Addresses[j].Address.Family != SocketAddressFamily.Internet6 && deviceAddress[1] != "0.0.0.0" )
					{
						selectedDevice = allDevices[i];
						if ( j > 0 && allDevices[i].Addresses[j - 1].Address.Family == SocketAddressFamily.Internet6 )
						{
							_isIPV6 = true;
						}
						break;
					}
				}
				if ( selectedDevice != null )
					break;
			}
		}

		public void StartReceive()
		{
			_isStart = true;
			_isActive = true;
			_threadForReceive = new Thread( Receive );
			_threadForReceive.Start();
		}

		private void Receive()
		{
			using( PacketCommunicator communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 ) )
			{
				//Console.WriteLine( "Listening on " + selectedDevice.Description + "..." );
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
								_isActive = true;
								//Console.Write( "ip = " + packet.Ethernet.Arp.SenderProtocolIpV4Address.ToString() + " mac = " + ByteArrayToHexViaByteManipulation( packet.Ethernet.Arp.SenderHardwareAddress.ToArray() ) );
								//Console.WriteLine();
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
						packet.Ethernet.IpV4.Tcp.DestinationPort == _tcpPort )
					{
						switch( result )
						{
							case PacketCommunicatorReceiveResult.Timeout:
								// Timeout elapsed
								continue;
							case PacketCommunicatorReceiveResult.Ok:
								PortReceived( this, new PortReceivedEventArgs( packet.Ethernet.IpV4.Source.ToString(), packet.Ethernet.IpV4.Tcp.SourcePort ) );
								_isActive = true;
								//Console.Write( "ip = " + packet.Ethernet.IpV4.Source.ToString() + " Port = " + packet.Ethernet.IpV4.Tcp.SourcePort.ToString() + " portDest : " + packet.Ethernet.IpV4.Tcp.DestinationPort.ToString() );
								//Console.WriteLine();
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
			timer.Close();
		}

		public void TimerToCallEndReceive()
		{
			timer.Start();
		}

		private void EndReceiveCallByTimer( object sender, EventArgs e )
		{
			if( !_isActive )
			{
				EndReceive();
				EndOfScan( this, null );
			}
			_isActive = false;
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
	public class PortReceivedEventArgs : EventArgs
	{
		public PortReceivedEventArgs( string ipAddress, ushort port )
		{
			IpAddress = ipAddress;
			Port = port;
		}

		public string IpAddress { get; private set; }
		public ushort Port { get; private set; }
	}
}
