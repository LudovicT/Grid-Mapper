using System;
using System.Net;
using System.Linq;
using System.Text;
// using System.Threading.Tasks; à utiliser plus tard pour accélérer le scanner
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GridMapper
{
    static public partial class NetworkUtilities
    {
        public static void ScanPort()
        {
            int StartingPort = 1; // à implémenter dans les args de la méthode plus tard
            int LastPort = 80;
            IPAddress IPAddr = Dns.GetHostEntry( "www.contoso.com" ).AddressList[0]; // exemple fourni dans MSDN

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