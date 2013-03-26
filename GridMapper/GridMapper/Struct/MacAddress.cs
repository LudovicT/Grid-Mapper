using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GridMapper.Struct
{
	[StructLayout( LayoutKind.Explicit )]
	public struct MacAddress
	{
		[FieldOffset( 0 )]
		public byte B0;
		[FieldOffset( 1 )]
		public byte B1;
		[FieldOffset( 2 )]
		public byte B2;
		[FieldOffset( 3 )]
		public byte B3;
		[FieldOffset( 4 )]
		public byte B4;
		[FieldOffset( 5 )]
		public byte B5;
		[FieldOffset( 0 )]
		public int MACAddress;

		public override string ToString()
		{
			return String.Format( "{0:X}:{1:X}:{2:X}:{3:X}", B0, B1, B2, B3 );
		}

		public override bool Equals( object obj )
		{
			if (obj is MacAddress)
			{
				return MACAddress == ( (MacAddress)obj ).MACAddress;
			}
			if (obj is int)
			{
				return MACAddress == (int)obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return MACAddress;
		}
	}
}
