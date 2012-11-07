using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using NCheckNetworks.NetworkModelObject;
using System.Windows.Forms;

namespace GridMapper
{
	public class Utilities
	{
		[DllImport( "iphlpapi.dll", ExactSpelling = true )]
		public static extern int SendARP( int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen );

		public static void taskGetMacAddress( List<IPAddress> Ips)
		{
			Task.Factory.StartNew( () => 
				Parallel.ForEach<IPAddress>( Ips, Ip => new AutoBuilder().MacAddressHandling( Ip, getMacAddress( Ip ) ) ) );
			//foreach( IPAddress Ip in Ips)
			//{
			//    Task.Factory.StartNew( () =>
			//    {
			//        new AutoBuilder().MacAddressHandling( Ip, getMacAddress( Ip ) );
			//    } );
			//}
		}

		public static PhysicalAddress getMacAddress(IPAddress dst)
		{
			int intAddress = BitConverter.ToInt32( dst.GetAddressBytes(), 0 );
			byte[] macAddr = new byte[6];
			uint macAddrLen = (uint)macAddr.Length;
			if ( SendARP( intAddress, 0, macAddr, ref macAddrLen ) != 0 )
				throw new InvalidOperationException( "SendARP failed." );

			string[] str = new string[(int)macAddrLen];
			for ( int i = 0; i < macAddrLen; i++ )
				str[i] = macAddr[i].ToString( "x2" );
			string fullMac = string.Join( "", str ).ToUpper();
			return PhysicalAddress.Parse(fullMac);
		}
	}
}
