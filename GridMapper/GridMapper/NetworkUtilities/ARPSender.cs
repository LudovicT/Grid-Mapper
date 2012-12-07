using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading.Tasks;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;

namespace GridMapper
{
	public class ARPSender
	{
		public event EventHandler<MacCompletedEventArgs> MacCompleted;

		[DllImport( "iphlpapi.dll", ExactSpelling = true )]
		static extern int SendARP( int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen );

		/// <summary>
		/// Get the MAC addresse from any given Ip which is reachable
		/// </summary>
		/// <param name="dst">The ip which we would like to get the MAC address</param>
		/// <returns>The MAC address</returns>
		public PhysicalAddress GetMac( IPAddress dst )
		{
			int intAddress = BitConverter.ToInt32( dst.GetAddressBytes(), 0 );
			byte[] macAddr = new byte[6];
			uint macAddrLen = (uint)macAddr.Length;
			if( SendARP( intAddress, 0, macAddr, ref macAddrLen ) != 0 )
			{
				throw new InvalidOperationException( "SendARP failed." );
			}

			string[] str = new string[(int)macAddrLen];
			for( int i = 0 ; i < macAddrLen ; i++ )
				str[i] = macAddr[i].ToString( "x2" );
			string fullMac = string.Join( "", str ).ToUpper();
			MacCompleted(this, new MacCompletedEventArgs( dst, PhysicalAddress.Parse( fullMac ) ) );
			return PhysicalAddress.Parse( fullMac );
		}

		public class MacCompletedEventArgs : EventArgs
		{
			PhysicalAddress _macAddress;
			IPAddress _ipAddress;

			internal MacCompletedEventArgs( IPAddress ipAddress, PhysicalAddress macAddress )
			{
				if( macAddress == null ) throw new NullReferenceException( "macAddress" );
				if( ipAddress == null ) throw new NullReferenceException( "ipAddress" );
				_macAddress = macAddress;
				_ipAddress = ipAddress;
			}

			public PhysicalAddress MacAddress { get { return _macAddress; } }
			public IPAddress IpAddress { get { return _ipAddress; } }	
		}
	}
}
