using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;

namespace GridMapper
{
	static class GetLocalInformation
	{
		public static void LocalMacAndIPAddress( out byte[] ip, out PhysicalAddress mac )
		{
			ip = new byte[4];
			mac = PhysicalAddress.None;
            LocalMac( out mac );
            LocalIPAddress( out ip );
		}
		public static void LocalMacAndIPAddress( out IPAddress ip, out PhysicalAddress mac )
		{
			ip = null;
			mac = PhysicalAddress.None;
            LocalMac( out mac );
            LocalIPAddress( out ip );
		}
		public static void LocalIPAddress( out byte[] ip )
		{
			ip = new byte[4];

			//get the interface ipv4
			foreach ( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					ip = ips.GetAddressBytes();
					break;
				}
			}
		}
        public static void LocalIPAddress( out IPAddress ip )
        {
            ip = null;
            //get the interface ipv4
            foreach( IPAddress ips in Dns.GetHostEntry( Environment.MachineName ).AddressList )
            {
                if( ips.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
                {
                    ip = ips;
                    break;
                }
            }
        }
		public static void LocalMac( out PhysicalAddress mac )
		{
			mac = PhysicalAddress.None;
			//get the interface
			NetworkInterface TrueNic = null;
			foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
			{
				if ( nic.OperationalStatus == OperationalStatus.Up )
				{
					TrueNic = nic;
					break;
				}
			}
			//get the interface mac
			mac = PhysicalAddress.Parse( TrueNic.GetPhysicalAddress().ToString() );
		}
	}
}
