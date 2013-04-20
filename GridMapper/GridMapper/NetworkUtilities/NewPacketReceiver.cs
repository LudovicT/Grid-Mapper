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
using System.Threading.Tasks;

namespace GridMapper.NetworkUtilities
{
	public class NewPacketReceiver
	{
		private List<IPacketReceiverClient> _observers = new List<IPacketReceiverClient>();

		PacketDevice selectedDevice;
		string _filter = string.Empty;
		ushort _tcpPort;
		readonly bool _isIPV6 = false;

		public static event EventHandler EndOfScan;

		PacketCommunicator _communicator; 

		public NewPacketReceiver( bool arp = true, bool tcp = true, ushort tcpPort = 62000, bool udp = false, ushort udpPort = 62001, int timeout = 5000)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			_tcpPort = tcpPort;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}

			if( arp || tcp )
			{
				PhysicalAddress mac;
				Byte[] ip;
				GetLocalInformation.LocalMacAndIPAddress( out ip, out mac );
				if( arp )
				{
					// arp is not comming from us
					string macPart1 = mac.ToString().Substring( 0, 4 ).ToUpper();
					string macPart2 = mac.ToString().Substring( 4, 4 ).ToUpper();
					string macPart3 = mac.ToString().Substring( 8, 4 ).ToUpper();
					_filter += "(arp && arp[8:2] != 0x" + macPart1 + " && arp[10:2] != 0x" + macPart2 + " && arp[12:2] != 0x" + macPart3 + ") ||";
				}
				if( tcp )
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

			_communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 );
		}

		public NewPacketReceiver( string filter )
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}

			_filter = filter;

			for( int i = 0; i < allDevices.Count; i++ )
			{
				for( int j = 0; j < allDevices[i].Addresses.Count; j++ )
				{
					string[] deviceAddress = allDevices[i].Addresses[j].Address.ToString().Split( ' ' );
					if( allDevices[i].Addresses[j].Address.Family != SocketAddressFamily.Internet6 && deviceAddress[1] != "0.0.0.0" )
					{
						selectedDevice = allDevices[i];
						if( j > 0 && allDevices[i].Addresses[j - 1].Address.Family == SocketAddressFamily.Internet6 )
						{
							_isIPV6 = true;
						}
						break;
					}
				}
				if( selectedDevice != null )
					break;
			}

			_communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 );
		}

		public void StartReceive()
		{
			Task.Factory.StartNew( () =>
			{
				Receive();
			} );
		}

		private void Receive()
		{
			_communicator.SetFilter( _filter );
			_communicator.ReceivePackets( 0, SendPacketsToClients );
		}

		public void EndReceive()
		{
			_communicator.Break();
		}

		//plus de timer utilisé
		public void TimerToCallEndReceive()
		{
		}

		//n'est plus utilisé pour le moment
		private void EndReceiveCallByTimer(object sender, EventArgs e)
		{
		}

		public void AttachClient( IPacketReceiverClient packetReceiverClient )
		{
			if (!_observers.Contains( packetReceiverClient )) _observers.Add( packetReceiverClient );
		}

		public void DetachClient( IPacketReceiverClient packetReceiverClient )
		{
			if (_observers.Contains( packetReceiverClient )) _observers.Remove( packetReceiverClient );
		}

		private void SendPacketsToClients( Packet packet )
		{
			foreach (IPacketReceiverClient obs in _observers) obs.Update( packet );
		}
	}
}

