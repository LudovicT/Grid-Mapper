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
	public class PortScannerWithConnect
	{
		public bool ScanPortWithConnect( IPAddress IPAddr, int PortToScan )
		{
			Socket s = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try
			{
				s.Connect( IPAddr, PortToScan );
				if( s.Connected == true ) // Port is in use and connection is successful
				{
					s.Close();
					return true;
				}
			}
			catch( SocketException ex )
			{
				if( ex.ErrorCode == 10061 )  // Port is unused and could not establish connection
				{
					s.Close();
					return false;
				}
			}
			return false;
		}
    }
}