using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using PcapDotNet.Packets;

namespace GridMapper.NetworkUtilities
{
	public class PacketSender
	{

		PacketDevice selectedDevice;
		PacketCommunicator outputCommunicator;

		public PacketSender()
		{
			IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

			if ( allDevices.Count == 0 )
			{
				Console.WriteLine( "No interfaces found! Make sure WinPcap is installed." );
				return;
			}
			selectedDevice = allDevices[0];

			outputCommunicator = selectedDevice.Open( 100, PacketDeviceOpenAttributes.Promiscuous, 1000 );
		}

		public void tryScanPort( Packet packetToSend )
		{
			outputCommunicator.SendPacket( packetToSend );
		}
	}
}
