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
using System.Collections;

namespace GridMapper
{
	public static class Arping
	{
		public static string GetARPaResult()
		{
			Process p = null;
			string output = string.Empty;

			try
			{
				p = Process.Start( new ProcessStartInfo( "arp", "-a" )
				{
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true
				} );

				output = p.StandardOutput.ReadToEnd();

				p.Close();
			}
			catch ( Exception ex )
			{
				throw new Exception( "Error Retrieving 'arp -a' Results", ex );
			}
			finally
			{
				if ( p != null )
				{
					p.Close();
				}
			}

			return output;
		}

		public static bool DeleteCachedARP()
		{
			bool state = true;

			try
			{
				Runcommand( "arp", "-d *" );
			}
			catch ( Exception ex )
			{
				state = false;
				throw new Exception( " Error executing 'arp -d *'", ex );
			}
			return state;
		}

		private static void Runcommand( string filename, string parameter )
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.FileName = filename;
			info.Arguments = parameter;
			info.CreateNoWindow = true;
			info.WindowStyle = ProcessWindowStyle.Hidden;
			Process p = Process.Start( info );
			p.WaitForExit( 5000 );

		}
	}
}
namespace GridMapper.NetworkRawSender
{
	public class arp
	{
		PacketDevice selectedDevice;
		EthernetLayer ethernetLayer;
		ArpLayer arpLayer;
		PacketCommunicator outputCommunicator;
		byte[] ip = new byte[4];
		PhysicalAddress mac = PhysicalAddress.None;
		public arp()
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

		public void tryARP(IEnumerable ipRange)
		{

			foreach ( int destIPint in ipRange )
			{
				outputCommunicator.SendPacket( BuildArpPacket( IPAddress.Parse( ( (uint)destIPint ).ToString() ).GetAddressBytes() ) );
				//Thread.SpinWait( 10000 );//divise par 3 ou 4 le débit
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
