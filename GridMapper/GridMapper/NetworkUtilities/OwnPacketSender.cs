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
		public readonly bool _isIPV6 = false;

		public OwnPacketSender(int nbPacketToSend = 10, int waitTime = 1)
		{
			selectedDevice = NetworkUtility.SelectedDevice;

			for( int j = 0; j < selectedDevice.Addresses.Count; j++ )
			{
				string[] deviceAddress = selectedDevice.Addresses[j].Address.ToString().Split( ' ' );
				if( selectedDevice.Addresses[j].Address.Family != SocketAddressFamily.Internet6 && deviceAddress[1] != "0.0.0.0" )
				{
					if( j > 0 && selectedDevice.Addresses[j - 1].Address.Family == SocketAddressFamily.Internet6 )
					{
						_isIPV6 = true;
					}
					break;
				}
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
			if( _waitTime <= 0 || _nbPacketToSend <= 0 || _isIPV6 )
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

		private void SendBuffer()
		{
			outputCommunicator.Transmit( _sendBuffer, false );
		}
	}
}
