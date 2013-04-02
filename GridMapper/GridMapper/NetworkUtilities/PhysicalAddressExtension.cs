using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper.NetworkUtilities
{
	public static class PhysicalAddressExtension
	{
		public static string ToMacString( this PhysicalAddress physicalAddress)
		{
			string temp = physicalAddress.ToString().ToUpperInvariant();
			var list = Enumerable
				.Range( 0, temp.Length / 2 )
				.Select( i => temp.Substring( i * 2, 2 ) );
			return string.Join( ":", list );
		}
	}
}
