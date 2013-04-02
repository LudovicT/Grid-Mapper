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
		bool _isStart;
		bool _isActive;
		System.Timers.Timer timer;
		readonly bool _isIPV6 = false;

		public static event EventHandler EndOfScan;

		public NewPacketReceiver( bool arp = true, bool tcp = true, ushort tcpPort = 62000, bool udp = false, ushort udpPort = 62001, int timeout = 5000)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
			_isStart = true;
			_isActive = true;
			_tcpPort = tcpPort;

			timer = new System.Timers.Timer();
			timer.Interval = timeout;
			timer.Enabled = true;
			timer.Elapsed += EndReceiveCallByTimer;
			timer.Stop();

			_isActive = true;

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
				string macPart1 = mac.ToString().Substring( 0, 4 ).ToUpper();
				string macPart2 = mac.ToString().Substring( 4, 4 ).ToUpper();
				string macPart3 = mac.ToString().Substring( 8, 4 ).ToUpper();
				if( arp )
				{
					// arp is not comming from us
					_filter += "(arp && arp[8:2] != 0x" + macPart1 + " && arp[10:2] != 0x" + macPart2 + " && arp[12:2] != 0x" + macPart3 + ")";
					if( tcp )
					{
						_filter += " || (tcp[tcpflags] & (tcp-ack) != 0 and tcp dst port " + tcpPort.ToString()
							+ " and not src " + new IPAddress( ip ).ToString() + ")";
					}
				}
				else if( tcp )
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
			Task.Factory.StartNew( () =>
			{
				Receive();
			} );
		}

		private void Receive()
		{
			using (PacketCommunicator communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness, 500 ))
			{
				communicator.SetFilter( _filter );
				int a = 0;
				do
				{
					Packet packet;
					PacketCommunicatorReceiveResult result = communicator.ReceivePacket( out packet );
					switch (result)
					{
						case PacketCommunicatorReceiveResult.Timeout:
							// Timeout elapsed
							continue;
						case PacketCommunicatorReceiveResult.Ok:
							Task.Factory.StartNew( () =>
							{
								NotifyObservers( packet );
							} );
							_isActive = true;
							break;
						default:
							throw new InvalidOperationException( "The result " + result + " should never be reached here" );
					}
				} while (_isStart);
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
		//n'est plus utilisé pour le moment
		private void EndReceiveCallByTimer( object sender, EventArgs e )
		{
			if( !_isActive )
			{
				EndReceive();
				EndOfScan( this, null );
			}
			_isActive = false;
		}

		public void Attach( IPacketReceiverClient packetReceiverClient )
		{
			if (!_observers.Contains( packetReceiverClient )) _observers.Add( packetReceiverClient );
		}

		public void Detach( IPacketReceiverClient packetReceiverClient )
		{
			if (_observers.Contains( packetReceiverClient )) _observers.Remove( packetReceiverClient );
		}

		private void NotifyObservers( Packet packet )
		{
			foreach (IPacketReceiverClient obs in _observers) obs.Update( packet );
		}
	}
}

