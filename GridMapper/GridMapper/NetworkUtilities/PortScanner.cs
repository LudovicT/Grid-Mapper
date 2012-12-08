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

        public bool ScanPort( IPAddress IPAddr, int PortToScan )
        {
			Socket s = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try
			{
				s.Connect(IPAddr, PortToScan);
				if ( s.Connected == true ) // Port is in use and connection is successful
				{
					//Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " OPEN");
				}
				s.Close();
				PortScanCompleted( this, new PortScanCompletedEventArgs( IPAddr, new PortComputer( PortToScan, true ) ) );
				return true;
			}
			catch ( SocketException ex )
			{
				if ( ex.ErrorCode == 10061 )  // Port is unused and could not establish connection
				{
					//Console.WriteLine(IPAddr.ToString() + ":" + PortToScan.ToString() + " CLOSE");
				}
				PortScanCompleted( this, new PortScanCompletedEventArgs( IPAddr, new PortComputer(PortToScan, false) ) );
				return false;
			}
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