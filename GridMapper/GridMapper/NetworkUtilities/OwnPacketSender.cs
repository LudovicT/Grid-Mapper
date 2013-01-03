using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.Threading;

namespace GridMapper.NetworkUtilities
{
	public class OwnPacketSender
	{

		PacketDevice selectedDevice;
		PacketCommunicator outputCommunicator;
		PacketSendBuffer _sendBuffer;
		readonly int _nbPacketToSend = 10;
		readonly int _waitTime = 1;
		readonly bool _isIPV6 = false;

		public OwnPacketSender(int nbPacketToSend = 10, int waitTime = 1)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			for(int i = 0; i < allDevices.Count; i++ )
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

			outputCommunicator = selectedDevice.Open( 100, PacketDeviceOpenAttributes.MaximumResponsiveness, 1000 );

			_nbPacketToSend = nbPacketToSend;
			_waitTime = waitTime;
			if ( _nbPacketToSend > 0 )
			{
				_sendBuffer = new PacketSendBuffer( (uint)( _nbPacketToSend * 100 ) );
			}
		}


		public void trySend( Packet packetToSend )
		{
			if ( !_isIPV6 )
			{
				if ( _waitTime <= 0 || _nbPacketToSend <= 0 )
				{
					outputCommunicator.SendPacket( packetToSend );
				}
				else if ( _nbPacketToSend > 0 && _waitTime > 0 )
				{
					_sendBuffer.Enqueue( packetToSend );
					if ( _sendBuffer.Length >= _nbPacketToSend )
					{
						SendBuffer();
						Thread.Sleep( _waitTime );
						_sendBuffer = new PacketSendBuffer( (uint)( _nbPacketToSend * 200 ) );
					}
				}
			}
			outputCommunicator.SendPacket( packetToSend );
			Thread.Sleep( 1 );

		}
		private void SendBuffer()
		{
			outputCommunicator.Transmit( _sendBuffer, false );
		}
	}
}
