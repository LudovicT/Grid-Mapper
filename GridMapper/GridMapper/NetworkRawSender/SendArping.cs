using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using System.Diagnostics;
using System.Threading;

namespace GridMapper.NetworkRawSender
{
	public class SendArping
	{
		public void Arping( IPAddress ipToSearchFor )
		{
			string ownMAC;
			string ownIP;
			getLocalMacAndIP( out ownIP, out ownMAC );
			char[] aDelimiter = { '.' };
			string[] aIPReso = ipToSearchFor.ToString().Split( aDelimiter, 4 );
			string[] aIPAddr = ownIP.Split( aDelimiter, 4 );

			byte[] oPacket = new byte[] { /*0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
						Convert.ToByte("0x" + ownMAC.Substring(0,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(2,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(4,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(6,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(8,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(10,2), 16),
						0x08, 0x06,*/ 0x00, 0x01,
						0x08, 0x00, 0x06, 0x04, 0x00, 0x01,
						Convert.ToByte("0x" + ownMAC.Substring(0,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(2,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(4,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(6,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(8,2), 16),
						Convert.ToByte("0x" + ownMAC.Substring(10,2), 16),
						Convert.ToByte(aIPAddr[0], 10),
						Convert.ToByte(aIPAddr[1], 10),
						Convert.ToByte(aIPAddr[2], 10),
						Convert.ToByte(aIPAddr[3], 10),
						0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
						Convert.ToByte(aIPReso[0], 10),
						Convert.ToByte(aIPReso[1], 10),
						Convert.ToByte(aIPReso[2], 10),
						Convert.ToByte(aIPReso[3], 10)};
			Socket arpSocket;
			arpSocket = new Socket( System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Raw );
			//arpSocket.SetSocketOption( SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true );
			arpSocket.EnableBroadcast = true;
			EndPoint localEndPoint = new IPEndPoint( IPAddress.Any, 0 );
			EndPoint remoteEndPoint = new IPEndPoint( ipToSearchFor, 0 );
			arpSocket.Bind( localEndPoint );
			arpSocket.BeginSendTo( oPacket,0,oPacket.Length,SocketFlags.None, remoteEndPoint, null, this);
			byte[] buffer = new byte[100];
			arpSocket.BeginReceiveMessageFrom( buffer, 0, 100, SocketFlags.None, ref remoteEndPoint, null, this);
		}

		public static void getLocalMacAndIP( out string ip, out string mac )
		{
			ip = string.Empty;
			mac = string.Empty;
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

			//get the interface ipv4
			foreach ( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					ip = ips.ToString();
					break;
				}
			}
			//get the interface mac
			mac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() ).ToString();
		}

	}
	public class winpcap
	{
		PacketDevice selectedDevice;
		EthernetLayer ethernetLayer;
		ArpLayer arpLayer;
		PacketCommunicator outputCommunicator;
		byte[] ip = new byte[4];
		PhysicalAddress mac = PhysicalAddress.None;
		public winpcap()
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];

			getLocalMacAndIP( out ip, out mac );
			ethernetLayer =
				new EthernetLayer
				{
					Source = new MacAddress( ToMac( mac.ToString() ) ),
					Destination = new MacAddress( "FF:FF:FF:FF:FF:FF" ),
					EtherType = EthernetType.None, // Will be filled automatically.
				};
			arpLayer =
				new ArpLayer
				{
					ProtocolType = EthernetType.IpV4,
					Operation = ArpOperation.Request,
					SenderHardwareAddress = Array.AsReadOnly( mac.GetAddressBytes().ToArray() ), // 03:03:03:03:03:03.
					SenderProtocolAddress = Array.AsReadOnly( ip ), // 1.2.3.4.
					TargetHardwareAddress = Array.AsReadOnly( new byte[] { 0, 0, 0, 0, 0, 0 } ), // 00:00:00:00:00:00.
				};

			outputCommunicator =
					selectedDevice.Open( 100, PacketDeviceOpenAttributes.Promiscuous, 1000 );
		}

		public static void interfaces()
		{
			// Retrieve the device list from the local machine
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}

			// Print the list
			for ( int i = 0; i != allDevices.Count; ++i )
			{
				LivePacketDevice device = allDevices[i];
				Console.Write( ( i + 1 ) + ". " + device.Name );
				if ( device.Description != null )
					Console.WriteLine( " (" + device.Description + ")" );
				else
					Console.WriteLine( " (No description available)" );
			}
		}
		/// <summary>
		/// This function build an ARP over Ethernet packet.
		/// </summary>
		private Packet BuildArpPacket( byte[] destIP )
		{
			arpLayer.TargetProtocolAddress = Array.AsReadOnly( destIP );

			PacketBuilder builder = new PacketBuilder( ethernetLayer, arpLayer );

			return builder.Build( DateTime.Now );
		}

		public static void getLocalMacAndIP( out byte[] ip, out PhysicalAddress mac )
		{
			ip = new byte[4];
			mac = PhysicalAddress.None;
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

			//get the interface ipv4
			foreach ( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					ip = ips.GetAddressBytes();
					break;
				}
			}
			//get the interface mac
			mac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() );
		}
		public void receiveARP()
		{
			using ( PacketCommunicator communicator =
				selectedDevice.Open( 42,                                  // portion of the packet to capture
				// 65536 guarantees that the whole packet will be captured on all the link layers
									PacketDeviceOpenAttributes.MaximumResponsiveness, // promiscuous mode
									500 ) )                                  // read timeout
			{
				Console.WriteLine( "Listening on " + selectedDevice.Description + "..." );

				// Retrieve the packets
				int nbPackets;
				//communicator.ReceivePackets(0, PacketHandler );

				Packet packet;
				do
				{
					PacketCommunicatorReceiveResult result = communicator.ReceivePacket( out packet );
					if ( packet != null && packet.Ethernet.EtherType == EthernetType.Arp && packet.Ethernet.Arp.Operation == ArpOperation.Reply )
					{
						switch ( result )
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
				} while ( true );
			}
		}
		private void PacketHandler( Packet packet )
		{
			if ( packet != null && packet.Ethernet.EtherType == EthernetType.Arp && packet.Ethernet.Arp.Operation == ArpOperation.Reply )
			{
				Console.Write( "ip = " + packet.Ethernet.Arp.SenderProtocolIpV4Address.ToString() + " mac = " + ByteArrayToHexViaByteManipulation( packet.Ethernet.Arp.SenderHardwareAddress.ToArray() ) );
				Console.WriteLine();
			}
		}

		private string ByteArrayToHexViaByteManipulation( byte[] bytes )
		{
			char[] c = new char[bytes.Length * 2];
			byte b;
			for ( int i = 0; i < bytes.Length; i++ )
			{
				b = ( (byte)( bytes[i] >> 4 ) );
				c[i * 2] = (char)( b > 9 ? b + 0x37 : b + 0x30 );
				b = ( (byte)( bytes[i] & 0xF ) );
				c[i * 2 + 1] = (char)( b > 9 ? b + 0x37 : b + 0x30 );
			}
			return new string( c );
		}

		public void tryARP(IPParserResult ipRange)
		{

			foreach ( int destIPint in ipRange.Result )
			{
				outputCommunicator.SendPacket( BuildArpPacket( IPAddress.Parse( ( (uint)destIPint ).ToString() ).GetAddressBytes() ) );
				Thread.SpinWait( 10000 );//divise par 3 ou 4 le débit
			}

			//uint i = 0;
			//if ( ipRange.Result != null )
			//{
			//    i = (uint)ipRange.Result.Count();
			//}
			//using ( PacketSendBuffer sendBuffer = new PacketSendBuffer( i * 100 ) )
			//{
			//    // Fill the buffer with the packets from the file
			//    int numPackets = 0;
			//    foreach ( int destIPint in ipRange.Result )
			//    {
			//        sendBuffer.Enqueue( BuildArpPacket( IPAddress.Parse( ( (uint)destIPint ).ToString() ).GetAddressBytes() ) );
			//        ++numPackets;
			//    }

			//    // Transmit the queue
			//    Stopwatch stopwatch = new Stopwatch();
			//    stopwatch.Start();
			//    long startTimeMs = stopwatch.ElapsedMilliseconds;
			//    Console.WriteLine( "Start Time: " + startTimeMs );
			//    outputCommunicator.Transmit( sendBuffer, true );
			//    long endTimeMs = stopwatch.ElapsedMilliseconds;
			//    Console.WriteLine( "End Time: " + endTimeMs );
			//    long elapsedTimeMs = endTimeMs - startTimeMs;
			//    Console.WriteLine( "Elapsed Time: " + elapsedTimeMs );
			//    double averagePacketsPerSecond = elapsedTimeMs == 0 ? double.MaxValue : (double)numPackets / elapsedTimeMs * 1000;

			//    Console.WriteLine( "Elapsed time: " + elapsedTimeMs + " ms" );
			//    Console.WriteLine( "Total packets generated = " + numPackets );
			//    Console.WriteLine( "Average packets per second = " + averagePacketsPerSecond );
			//    Console.WriteLine();
			//}
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
