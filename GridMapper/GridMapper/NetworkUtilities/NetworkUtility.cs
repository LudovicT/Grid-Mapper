﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper.NetworkUtilities
{
	public static class NetworkUtility
	{
		public static string ByteArrayToHexViaByteManipulation( byte[] bytes )
		{
			char[] c = new char[bytes.Length * 2];
			byte b;
			for( int i = 0; i < bytes.Length; i++ )
			{
				b = ((byte)(bytes[i] >> 4));
				c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(bytes[i] & 0xF));
				c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			}
			return new string( c );
		}
	}
}
