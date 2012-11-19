using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper
{
	static public partial class NetworkUtilities
	{
		private static void IpRange(int ip, int maskBits)
		{
			var mask = ~((1 << (32 - maskBits)) - 1);
			var StartIP = ip & mask;
			var EndIP = (ip & mask) | ~mask;
			Console.WriteLine( IPAddress.Parse(StartIP.ToString()) + " " + IPAddress.Parse(EndIP.ToString()) );
		}
		public static void IpRange( IPAddress ip, int maskBits )
		{
			byte[] addressBytes = ip.GetAddressBytes();
			int intIP = addressBytes[0] * 16777216 + addressBytes[1] * 65536 + addressBytes[2] * 256 + addressBytes[3];
			IpRange( intIP, maskBits );
		}
	}
}
