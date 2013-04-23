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

        public void Update( Packet packet )
        {
            IpV4Datagram datagram = packet.Ethernet.IpV4;
            if ((datagram.Source == _ipV4AddressTarget || datagram.Destination == _ipV4AddressTarget)
                && (datagram.Tcp.DestinationPort == 80
                    || datagram.Tcp.DestinationPort == 8080
                    || datagram.Tcp.SourcePort == 80
                    || datagram.Tcp.SourcePort == 8080))
            {
                //List<Packet> httpList = new List<Packet>();
                //httpList.Add(packet);
                var http = datagram.Tcp.HttpCollection;
                Console.WriteLine( http.Count );
            }
        }
    }
}
