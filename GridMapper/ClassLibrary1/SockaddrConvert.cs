using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication5
{
	class SockaddrConvert
	{
		/// <summary>
		/// This routine converts an IPEndPoint into a byte array that represents the
		/// underlying sockaddr structure of the correct type. Currently this routine
		/// supports only IPv4 and IPv6 socket address structures.
		/// </summary>
		/// <param name="endPoint">IPEndPoint to convert to a binary form</param>
		/// <returns>Binary array of the serialized socket address structure</returns>
		static public byte[] GetSockaddrBytes( IPEndPoint endPoint )
		{
			SocketAddress socketAddress = endPoint.Serialize();
			byte[] sockaddrBytes;
			sockaddrBytes = new byte[socketAddress.Size];
			for( int i = 0 ; i < socketAddress.Size ; i++ )
			{
				sockaddrBytes[i] = socketAddress[i];
			}
			return sockaddrBytes;
		}

		/// <summary>
		/// This routine converts the binary representation of a sockaddr structure back
		/// into an IPEndPoint object. This is done by looking at the first 2 bytes of the
		/// serialized byte array which always indicate the address family of the underlying
		/// structure. From this we can construct the appropriate IPEndPoint object.
		/// </summary>
		/// <param name="sockaddrBytes"></param>
		/// <returns></returns>
		static public IPEndPoint GetEndPoint( byte[] sockaddrBytes )
		{
			IPEndPoint unpackedEndpoint = null;
			IPAddress unpackedAddress;
			ushort addressFamily, unpackedPort;
			// Reconstruct the 16-bit (short) value representing the address family  
			addressFamily = BitConverter.ToUInt16( sockaddrBytes, 0 );
			if( addressFamily == 2 )   // AF_INET
			{
				byte[] addressBytes = new byte[4];
				unpackedPort = BitConverter.ToUInt16( sockaddrBytes, 2 );
				unpackedAddress = new IPAddress( BitConverter.ToUInt32( sockaddrBytes, 4 ) );
				unpackedEndpoint = new IPEndPoint( unpackedAddress, unpackedPort );
			}
			else if( addressFamily == 23 )     // AF_INET6
			{
				byte[] addressBytes = new byte[16];
				unpackedPort = BitConverter.ToUInt16( sockaddrBytes, 2 );
				Array.Copy( sockaddrBytes, 8, addressBytes, 0, 16 );
				unpackedAddress = new IPAddress( addressBytes );
				unpackedEndpoint = new IPEndPoint( unpackedAddress, unpackedPort );
			}
			else
			{
				Console.WriteLine( "GetEndPoint: Unknown address family: {0}", addressFamily );
			}
			return unpackedEndpoint;
		}
	}
}
