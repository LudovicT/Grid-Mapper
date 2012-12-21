using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GridMapper
{
	public class IPRange
	{
		public static uint IPAddressToUint(IPAddress ip)
		{
			byte[] addressBytes = ip.GetAddressBytes();
			uint intIP;
			if ( BitConverter.IsLittleEndian )
			{
				intIP =(uint)( addressBytes[0] * 16777216 + addressBytes[1] * 65536 + addressBytes[2] * 256 + addressBytes[3]);
			}
			else
			{
				intIP =(uint) (addressBytes[3] * 16777216 + addressBytes[2] * 65536 + addressBytes[1] * 256 + addressBytes[0]);
			}
			return intIP;
		}

		public static List<uint> IpRange( IPAddress ip, int maskBits )
		{
			int intIP = 0;
			intIP = (int)IPAddressToUint( ip );
			int mask = ~( ( 1 << ( 32 - maskBits ) ) - 1 );
			int StartIP = intIP & mask;
			int EndIP = ( intIP & mask ) | ~mask;
			List<uint> IPs = new List<uint>();

			//avoid/prevent an infinite loop
			if ( StartIP < 0 )
			{
				var temp = EndIP;
				EndIP = StartIP;
				StartIP = temp;
			}
			uint startIP = (uint)StartIP;
			uint endIP = (uint)EndIP;
			if ( startIP < endIP )
			{
				IPs.Add( startIP );
				IPs.Add( endIP );
			}
			else if ( startIP > endIP )
			{
				IPs.Add( endIP );
				IPs.Add( startIP );
			}
			return IPs;
		}
		public static IPParserResult AutoIpRange()
		{
			List<uint> uip = new List<uint>();
			IPAddress machineIP = IPAddress.None;
			foreach ( IPAddress ip in System.Net.Dns.GetHostEntry( Environment.MachineName ).AddressList )
			{
				if ( ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
				{
					machineIP = ip;
				}
			}
			uip = IpRange( machineIP, SubnetMaskToCIDR( GetSubnetMask( machineIP ) ) );
			IPAddress ip1 = IPAddress.Parse(uip[0].ToString());
			IPAddress ip2 = IPAddress.Parse(uip[1].ToString());
			string StringToTest = ip1.ToString() + "-" + ip2.ToString();
			return IPParser.TryParse( StringToTest );
		}

		static int SubnetMaskToCIDR(IPAddress IP)
		{
			byte[] IPByte = IP.GetAddressBytes();
			int CIDR = -1;

			int bitCount = 0;
			int i = 0;
			while ( i <= 3 )
			{
				if (IPByte[i] == 255 )
				{
					i++;
					continue;
				}
				else
				{
					for ( int j = 0; j < 8; j++ )
					{
						if ( ( IPByte[i] & Convert.ToInt32( ( Math.Pow( 2, j ) ) ) ) != 0 )
						{
							bitCount++;
						}
					}
					CIDR = bitCount + 8*i;
					break;
				}
			}
			if ( i == 4 && CIDR == -1)
			{
				throw new InvalidOperationException();
			}
			return CIDR;
		}

		static IPAddress GetSubnetMask( IPAddress address )
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
