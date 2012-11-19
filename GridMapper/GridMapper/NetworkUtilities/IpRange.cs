using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GridMapper
{
	static public partial class NetworkUtilities
	{
		public static List<IPAddress> IpRange( IPAddress ip, int maskBits )
		{
			byte[] addressBytes = ip.GetAddressBytes();
			int intIP = addressBytes[0] * 16777216 + addressBytes[1] * 65536 + addressBytes[2] * 256 + addressBytes[3];
			var mask = ~( ( 1 << ( 32 - maskBits ) ) - 1 );
			var StartIP = intIP & mask;
			var EndIP = ( intIP & mask ) | ~mask;
			List<IPAddress> IPs = new List<IPAddress>();
			for ( int i = StartIP; i <= EndIP; i++ )
			{
				IPs.Add( IPAddress.Parse( i.ToString() ) );
			}
			return IPs;
		}
		public static List<IPAddress> AutoIpRange()
		{
			IPAddress machineIP = IPAddress.None;
			foreach ( IPAddress ip in System.Net.Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					machineIP = ip;
				}
			}
			return IpRange( machineIP, SubnetMaskToCIDR(GetSubnetMask( machineIP )) );
		}

		private static int SubnetMaskToCIDR(IPAddress IP)
		{
			byte[] IPByte = IP.GetAddressBytes();
			int CIDR = -1;

			int i = 0;
			while ( i < 3 )
			{
				if ( ( IPByte[i] == 0 || IPByte[i] == 255 ) )
				{
					i++;
					continue;
				}
				else
				{
					int.TryParse(Math.Sqrt(256 - IPByte[i]).ToString(),out CIDR);
					break;
				}
			}
			if ( i == 3 && CIDR == -1)
			{
				throw new OperationCanceledException();
			}
			return (8*(i+1) - CIDR);
		}

		private static IPAddress GetSubnetMask( IPAddress address )
		{
			foreach ( NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces() )
			{
				foreach ( UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses )
				{
					if ( unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork )
					{
						if ( address.Equals( unicastIPAddressInformation.Address ) )
						{
							return unicastIPAddressInformation.IPv4Mask;
						}
					}
				}
			}
			throw new ArgumentException( string.Format( "Can't find subnetmask for IP address '{0}'", address ) );
		}
	}
}
