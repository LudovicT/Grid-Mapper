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
        string _filter = string.Empty;
        //Thread _threadForReceive;

        public static event EventHandler EndOfScan;

        public PacketListenerTarget(IPAddress target)
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine; // Retrieves the device list from the local machine
            _isStart = true;
			_isActive = true;
            //_tcpPort = tcpPort;

            if (allDevices.Count == 0)
            {
                //No interface found!
                return;
            }
            // Selects all IPv4 HTTP packets 
            // packets to and from port 80 
            // packets to and from targetted IP 
            // NOT packets SYN and FIN and ACK-only packets (so, only data)
            _filter += "tcp port 80 and (((ip[2:2] - ((ip[0]&0xf)<<2)) - ((tcp[12]&0xf0)>>2)) != 0) and net " + target + ""; 
            
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

		private void Watch()
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
                communicator.SetFilter(_filter);// Compiling and setting the filter
                communicator.ReceivePackets(0, PacketHandler);// starting capture
            }
        }
        // Callback function invoked by libpcap for every incoming packet
        private static void PacketHandler(Packet packet)
        {
            //packet.Ethernet.IpV4.Transport.DestinationPort;
            //packet.Ethernet.IpV4.Transport.SourcePort;
            //packet.Ethernet.IpV4.Source;
            //packet.Ethernet.IpV4.Destination;
            //packet.Ethernet.IpV4.Protocol;
        }
    }
    
}

