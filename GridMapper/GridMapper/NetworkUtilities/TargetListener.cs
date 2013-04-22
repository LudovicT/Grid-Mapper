using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper.NetworkUtilities
{
    public class TargetListener : IPacketReceiverClient
    {
        IPAddress _target;
        IpV4Address _ipV4AddressTarget;

        public TargetListener( string iptarget )
        {
            if ( !IPAddress.TryParse( iptarget, out _target ) )
            {
                throw new ArgumentException( "Can't parse, string invalid format" );
            }
            else
            {
                _ipV4AddressTarget = new IpV4Address( iptarget );
            }
        }

        public void Update( PcapDotNet.Packets.Packet packet )
        {

            if ( (packet.Ethernet.IpV4.Source == _ipV4AddressTarget
                || packet.Ethernet.IpV4.Destination == _ipV4AddressTarget)
                &&
                (packet.Ethernet.IpV4.Tcp.DestinationPort == 80
                || packet.Ethernet.IpV4.Tcp.DestinationPort == 8080
                || packet.Ethernet.IpV4.Tcp.SourcePort == 80
                || packet.Ethernet.IpV4.Tcp.SourcePort == 8080)
              )
              save!!
                  packet.Ethernet.IpV4.Tcp.HttpCollection
        }
    }
}
