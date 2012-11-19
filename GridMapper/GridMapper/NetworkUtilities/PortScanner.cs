using System;
using System.Net;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net.Sockets;

namespace GridMapper
{
    static public partial class NetworkUtilities
    {
        public static void ScanPort( IPAddress IPAddr, int StartingPort, int LastPort )
        {
            for (int PortToScan = StartingPort; PortToScan <= LastPort; PortToScan++)
            {
                ScanPort( IPAddr, PortToScan );
            }
        }
        public static void ScanPort( IPAddress IPAddr, int PortToScan )
        {
            TcpClient TcpScanner = new TcpClient();
            try
            {
                TcpScanner.Connect(IPAddr, PortToScan);
                Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN");
            }
            catch
            {
                Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE");
            }
        }
    }
}