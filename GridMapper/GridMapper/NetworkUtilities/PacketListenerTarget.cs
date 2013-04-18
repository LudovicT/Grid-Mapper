using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Transport;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace GridMapper.NetworkUtilities
{
    public class PacketListenerTarget
    {
        readonly bool _isIPV6 = false;
        ushort _tcpPort;
		bool _isStart;
		bool _isActive;
   		PacketDevice selectedDevice;
        //Thread _threadForReceive;


        public PacketListenerTarget()
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine; // Retrieve the device list from the local machine
            _isStart = true;
			_isActive = true;
            //_tcpPort = tcpPort;

            if (allDevices.Count == 0)
            {
                //No interface found!
                return;
            }

            for (int i = 0; i < allDevices.Count; i++)
		    {
		        for (int j = 0; j < allDevices[i].Addresses.Count; j++)
			    {
			        string[] deviceAddress = allDevices[i].Addresses[j].Address.ToString().Split(' ');
				    if (allDevices[i].Addresses[j].Address.Family != SocketAddressFamily.Internet6 && deviceAddress[1] != "0.0.0.0")
				    {
					    selectedDevice = allDevices[i];
					    if (j > 0 && allDevices[i].Addresses[j - 1].Address.Family == SocketAddressFamily.Internet6)
					    {
						    _isIPV6 = true;
					    }
					    break;
				     }
                }
                if ( selectedDevice != null )
                {
			        break;
			    }   
            }
        }
        
        public void StartReceive()
		{
			_isStart = true;
			_isActive = true;
            //_threadForReceive = new Thread( Receive );
            //_threadForReceive.Start();
		}

		private void Receive()
		{
            using (PacketCommunicator communicator =                        // Opening the device
                                    selectedDevice.Open(65536,              // portion of the packet to capture (65536 = full packet capture)
                                    PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                    1000))                                  // read timeout
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)// Check the link layer. Ethernet only
                {
                    //Error management (!)
                    return;
                }
                communicator.SetFilter("ip and tcp");// Compiling and setting the first filter
                communicator.ReceivePackets(0, PacketHandler);// starting capture
            }
        }
        // Callback function invoked by libpcap for every incoming packet
        private static void PacketHandler(Packet packet)
        {
            //Second filter (by IP target and port destination) 
            //Making sure the packet's transfert is completed as well
            IpV4Datagram ip = packet.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;
        }
    }
    
}

