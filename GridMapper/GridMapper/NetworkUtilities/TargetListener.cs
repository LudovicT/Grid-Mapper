using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Core;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Http;

namespace GridMapper.NetworkUtilities
{
    public class TargetListener : IPacketReceiverClient
    {
        IPAddress _target;
        IpV4Address _ipV4AddressTarget;
        private static Queue<IpV4Datagram> requestQueue = new Queue<IpV4Datagram>(30); 

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
            //IpV4Datagram datagram = packet.Ethernet.IpV4;
            //if ((datagram.Source == _ipV4AddressTarget || datagram.Destination == _ipV4AddressTarget)
            //    && (datagram.Tcp.DestinationPort == 80
            //        || datagram.Tcp.DestinationPort == 8080
            //        || datagram.Tcp.SourcePort == 80
            //        || datagram.Tcp.SourcePort == 8080))
            //{
            //    //List<Packet> httpList = new List<Packet>();
            //    //httpList.Add(packet);
            //    var http = datagram.Tcp.HttpCollection;
            //    Console.WriteLine( http.Count );
            //}
            try
            {
                if (packet.Ethernet.EtherType == EthernetType.IpV4 &&
                    packet.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp &&
                    packet.Ethernet.IpV4.Tcp.Http != null)
                {
                    //Pulling the HTTP layer
                    var http2 = ((PcapDotNet.Packets.Http.HttpLayer)((((packet.Ethernet).IpV4).Tcp).Http.ExtractLayer()));

                    //Checking whether it's a request or response
                    if (http2 != null && http2.Header != null && http2.Header.BytesLength > 0)
                    {
                        if (http2 is HttpRequestLayer)
                        {
                            requestQueue.Enqueue((packet.Ethernet).IpV4);
                        }
                        else if (http2 is HttpResponseLayer && http2.Header.ContentType != null && http2.Header.ContentType.MediaSubtype != null
                            && (http2.Header.ContentType.MediaSubtype == "html"))
                        {
                            //If everything's OK, HTTP response regular status code must be 200
                            if ((packet.Ethernet.IpV4.Tcp.Http as HttpResponseDatagram).StatusCode == 200)
                            {
                                //Checking whether there is a request packet to match this response packet
                                var lst = requestQueue.Where(p =>
                                    p.Source.Equals(packet.Ethernet.IpV4.Destination) &&
                                    p.Destination.Equals(packet.Ethernet.IpV4.Source) &&
                                    p.Tcp.DestinationPort.Equals(packet.Ethernet.IpV4.Tcp.SourcePort) &&
                                    p.Tcp.SourcePort.Equals(packet.Ethernet.IpV4.Tcp.DestinationPort) &&
                                    p.Tcp.NextSequenceNumber.Equals(packet.Ethernet.IpV4.Tcp.AcknowledgmentNumber)).ToList();

                                var request = lst.FirstOrDefault();
                                //To be continued...
                                if (request != null)
                                    Console.WriteLine(((HttpRequestLayer)((request.Tcp.Http as HttpDatagram).ExtractLayer())).Uri);
                                //HttpTranscode class does not exist !
                                HttpTranscode trans = new HttpTranscode( http2.Header, http2.Body.ToMemoryStream().ToArray() );
                                string test2 = trans.DecodeBody();
                                if (!string.IsNullOrEmpty(test2))
                                    Console.WriteLine(test2);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
        }
}

