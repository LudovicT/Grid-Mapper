using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
	public class WinsockIoctl
	{
		/// <summary>
		/// An interface query takes the socket address of a remote destination and
		/// returns the local interface that destination is reachable on.
		/// </summary>
		public const int SIO_ROUTING_INTERFACE_QUERY = -939524076;  // otherwise equal to 0xc8000014

		/// <summary>
		/// The address list query returns a list of all local interface addresses.
		/// </summary>
		public const int SIO_ADDRESS_LIST_QUERY = 0x48000016;
	}
}
