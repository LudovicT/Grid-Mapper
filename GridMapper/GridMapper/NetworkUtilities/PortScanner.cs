using System;
using System.Net;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GridMapper
{
    static public partial class NetworkUtilities
    {
        public static void ScanPort( IPAddress IPAddr, int StartingPort, int LastPort )
        {
          //  IPAddress IPAddr = Dns.GetHostEntry( "www.contoso.com" ).AddressList[0];

            for ( int PortToCheck = StartingPort; PortToCheck <= LastPort; PortToCheck++ )
            {
                TcpClient TcpScanner = new TcpClient();
                try
                {
                    TcpScanner.Connect( IPAddr, PortToCheck );
                    Console.WriteLine( IPAddr.ToString() + ":" + PortToCheck.ToString() + " OPEN");
                    Console.ReadKey();
                }
                catch
                {
                    Console.WriteLine(IPAddr.ToString() + ":" + PortToCheck.ToString() + " CLOSE");
                    Console.ReadKey();
                }
            }
        }
    }
}