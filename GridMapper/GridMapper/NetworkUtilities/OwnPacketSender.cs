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
		int _numPackets;
		PacketSendBuffer _sendBuffer;
		readonly int _nbPacketToSend;
		readonly int _waitTime;

		public OwnPacketSender(int nbPacketToSend = 10, int waitTime = 1)
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];

			outputCommunicator = selectedDevice.Open( 100, PacketDeviceOpenAttributes.Promiscuous, 1000 );

			_nbPacketToSend = nbPacketToSend;
			_waitTime = waitTime;
			if ( _nbPacketToSend > 0 )
			{
				_sendBuffer = new PacketSendBuffer( (uint)( _nbPacketToSend * 100 ) );
			}
		}


		public void trySend( Packet packetToSend )
		{
			if ( _waitTime <= 0 || _nbPacketToSend <= 0 && _waitTime > 0 )
			{
				outputCommunicator.SendPacket( packetToSend );
			}
			else if ( _nbPacketToSend > 0 && _waitTime > 0 )
			{
				_sendBuffer.Enqueue( packetToSend );
				++_numPackets;
				if ( _numPackets == _nbPacketToSend )
				{
					SendBuffer();
					Thread.Sleep(_waitTime);
				}
			}

		}
		private void SendBuffer()
		{
			outputCommunicator.Transmit( _sendBuffer, false );
		}
	}
}
