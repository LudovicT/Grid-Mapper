using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GridMapper
{
	[StructLayout( LayoutKind.Explicit )]
	public struct IPAddressV4
	{
		[FieldOffset( 0 )]
		public byte B0;
		[FieldOffset( 1 )]
		public byte B1;
		[FieldOffset( 2 )]
		public byte B2;
		[FieldOffset( 3 )]
		public byte B3;
		[FieldOffset( 0 )]
		public int Address;

		public override string ToString()
		{
			return String.Format( "{0}.{1}.{2}.{3}", B0, B1, B2, B3 );
		}

		public override bool Equals( object obj )
		{
			if( obj is IPAddressV4 )
			{
				return Address == ((IPAddressV4)obj).Address;
			}
			if( obj is int )
			{
				return Address == (int)obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Address;
		}
	}
}
