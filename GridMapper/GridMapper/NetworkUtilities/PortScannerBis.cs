using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using System.Net.NetworkInformation;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;
using System.Net;
using PcapDotNet.Packets.Arp;
using System.Threading;

namespace GridMapper.NetworkUtilities
{
	public class PortScannerBis
	{
		PacketDevice selectedDevice;
		PayloadLayer payloadLayer;
		TcpLayer tcpLayer;
		IpV4Layer ipV4Layer;
		EthernetLayer ethernetLayer;
		PacketCommunicator outputCommunicator;

		byte[] ip = new byte[4];
		PhysicalAddress mac = PhysicalAddress.None;

		public PortScannerBis()
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];

			getLocalMacAndIP( out ip, out mac );

			outputCommunicator = selectedDevice.Open( 100, PacketDeviceOpenAttributes.Promiscuous, 1000 );
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
		private Packet BuildTcpPacket(string ipAddress, string macAddress, int portToScan )
		{
			ethernetLayer = new EthernetLayer
								{
									Source = new MacAddress( ToMac( mac.ToString() ) ),
									Destination = new MacAddress( ToMac( macAddress.ToString() ) ),
									EtherType = EthernetType.None, // Will be filled automatically.
								};

			ipV4Layer = new IpV4Layer
							{
								Source = new IpV4Address( new IPAddress( ip ).ToString() ),
								CurrentDestination = new IpV4Address( ipAddress ),
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
								DestinationPort = (ushort)portToScan,
								Checksum = null, // Will be filled automatically.
								SequenceNumber = 100,
								AcknowledgmentNumber = 0,
								ControlBits = TcpControlBits.Synchronize,
								Window = 8192,
								UrgentPointer = 0,
								Options = new TcpOptions(new TcpOptionMaximumSegmentSize(1460),TcpOption.Nop, new TcpOptionWindowScale(2),new TcpOptionSelectiveAcknowledgmentPermitted(), new TcpOptionTimestamp((uint)DateTime.Now.Ticks,0)),
							};
			payloadLayer = new PayloadLayer
			{
				Data = new Datagram( Encoding.ASCII.GetBytes( "test" ) ),
			};

			

			PacketBuilder builder = new PacketBuilder( ethernetLayer, ipV4Layer, tcpLayer, payloadLayer );

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

		public void receivePortPacketScanner()
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
						try
						{
							PacketCommunicatorReceiveResult result = communicator.ReceivePacket( out packet );
							if( packet != null && packet.Ethernet.IpV4 != null && packet.Ethernet.IpV4.Tcp != null && packet.Ethernet.IpV4.Tcp.IsAcknowledgment && packet.Ethernet.IpV4.Tcp.IsSynchronize && packet.Ethernet.IpV4.Tcp.DestinationPort == 62000 )
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
						}
						catch( Exception e )
						{
						}
					} while( true );
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

		public void tryScanPort(string ipAddress, string macAddress, int portToScan)
		{
				outputCommunicator.SendPacket( BuildTcpPacket( ipAddress,  macAddress,  portToScan ) );
				if ( portToScan % 10 == 0 )
				{
					Thread.Sleep( 1 );
				}
				//Thread.SpinWait( 10000 );//divise par 3 ou 4 le débit

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
