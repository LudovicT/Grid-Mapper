using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GridMapper
{
	public class Utilities
	{
		[DllImport( "iphlpapi.dll", ExactSpelling = true )]
		public static extern int SendARP( int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen );

		public static String getMacAddress(IPAddress dst)
		{
			//IPAddress dst = IPAddress.Parse( "192.168.2.1" );
			int intAddress = BitConverter.ToInt32( dst.GetAddressBytes(), 0 );
			byte[] macAddr = new byte[6];
			uint macAddrLen = (uint)macAddr.Length;
			if ( SendARP( intAddress, 0, macAddr, ref macAddrLen ) != 0 )
				throw new InvalidOperationException( "SendARP failed." );

			string[] str = new string[(int)macAddrLen];
			for ( int i = 0; i < macAddrLen; i++ )
				str[i] = macAddr[i].ToString( "x2" );

			return string.Join( ":", str );
		}
	}
}
