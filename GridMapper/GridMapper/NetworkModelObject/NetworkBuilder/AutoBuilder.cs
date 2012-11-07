using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace NCheckNetworks.NetworkModelObject
{
	class AutoBuilder : Builder
	{
		static INetwork _network;

		public AutoBuilder()
		{
			if(_network == null)
				_network = new Network();
		}

		public void SuperPingerHandling( PingReply pingReply )
		{
			//test if exist interface with this ip
			NetworkInterface networkInterface = new NetworkInterface(pingReply.Address, pingReply.RoundtripTime);
			Host host = new Host( networkInterface );
			_network.NetworkHost.Add( host );
		}

		public void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			Console.WriteLine( ipAddress + macAddress );
		}
	}
}
