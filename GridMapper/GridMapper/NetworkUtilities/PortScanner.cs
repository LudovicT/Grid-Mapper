using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net.Sockets;

namespace GridMapper
{
    static public partial class NetworkUtilities
    {
		public static Task TaskScanPort(IPAddress IPAddr, int StartingPort, int LastPort )
		{
			Task task = Task.Factory.StartNew( () =>
			{
				for (int PortToScan = StartingPort; PortToScan <= LastPort; PortToScan++)
				{
					ScanPort( IPAddr, PortToScan );
				}
			} );
			return task;
		}

        public static Task ScanPort( IPAddress IPAddr, int PortToScan )
        {
			Task task = Task.Factory.StartNew( () =>
			{
				// METHODE DE VINCENT
				Socket s = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				try
				{
					s.Connect(IPAddr, PortToScan);
					if (s.Connected == true) // Port is in use and connection is successful
						Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN");
					s.Close();
				}
				catch ( SocketException ex )
				{
					if ( ex.ErrorCode == 10061 )  // Port is unused and could not establish connection
						Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE");
				}

				// METHOE DE FRANCK
				//Socket TcpScanner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//TcpScanner.Connect(IPAddr, PortToScan);
				//TcpScanner.ReceiveTimeout = 20;

				//if( !TcpScanner.Connected )
				//{
				//    Console.WriteLine( IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE" );
				//}
				//if( TcpScanner.Poll( -1, SelectMode.SelectWrite ) )
				//{
				//    Console.WriteLine( IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN" );
				//}
				//else
				//{
				//    if( TcpScanner.Poll( -1, SelectMode.SelectRead ) )
				//    {
				//        Console.WriteLine( IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN READ" );
				//    }
				//    else
				//    {
				//        if( TcpScanner.Poll( -1, SelectMode.SelectError ) )
				//        {
				//            Console.WriteLine( IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE" );
				//        }
				//    }
				//}
			} );
			return task;
        }
    }
}