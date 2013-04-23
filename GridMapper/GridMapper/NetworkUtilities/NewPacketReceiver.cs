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
			selectedDevice = NetworkUtility.SelectedDevice;

			_tcpPort = tcpPort;

			_communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness | PacketDeviceOpenAttributes.Promiscuous, 500 );
		}

		public NewPacketReceiver( string filter )
		{
			selectedDevice = NetworkUtility.SelectedDevice;

			_filter = filter;

			_communicator = selectedDevice.Open( 65536, PacketDeviceOpenAttributes.MaximumResponsiveness | PacketDeviceOpenAttributes.Promiscuous, 500 );
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

