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
    public class PortScanner
    {
		public event EventHandler<PortScanCompletedEventArgs> PortScanCompleted;

		public PortComputer ScanPort( IPAddress IPAddr, int PortToScan )
        {
			Socket s = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try
			{
				s.Connect(IPAddr, PortToScan);
				if ( s.Connected == true ) // Port is in use and connection is successful
				{
					PortComputer port = new PortComputer( PortToScan, true );
					//Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN");
					//PortScanCompleted( this,  new PortScanCompletedEventArgs( IPAddr, port ) );
					s.Close();
					return port;
				}
			}
			catch ( SocketException ex )
			{
				if ( ex.ErrorCode == 10061 )  // Port is unused and could not establish connection
				{
					PortComputer port = new PortComputer( PortToScan, false );
					//Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE");
					//PortScanCompleted( this, new PortScanCompletedEventArgs( IPAddr, port ) );
					s.Close();
					return port;
				}
			}
			return new PortComputer();
        }

		public class PortScanCompletedEventArgs : EventArgs
		{
			IPAddress _ipAddress;
			PortComputer _portComputer;
			bool _status;

			public PortScanCompletedEventArgs( IPAddress ipAddress, PortComputer portComputer )
			{
				if( ipAddress == null ) throw new NullReferenceException( "ipAddress" );
				_ipAddress = ipAddress;
				_portComputer = portComputer;
			}

			public PortComputer Port { get { return _portComputer; } }
			public IPAddress IpAddress { get { return _ipAddress; } }
		}
    }
}