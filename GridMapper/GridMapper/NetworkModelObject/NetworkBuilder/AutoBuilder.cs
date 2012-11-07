using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace GridMapper.NetworkModelObject
{
	class AutoBuilder : Builder
	{
		
		public AutoBuilder()
		{
			if(_network == null)
				_network = new Network();
			if( _networkInterfaceDictionary == null )
				_networkInterfaceDictionary = new Dictionary<IPAddress, INetworkInterface>();
		}

		public void SuperPingerHandling( PingReply pingReply )
		{
			//test if exist interface with this ip
			NetworkInterface networkInterface = new NetworkInterface(pingReply.Address, pingReply.RoundtripTime);
			Host host = new Host( networkInterface );
			_network.NetworkHost.Add( host );
			_networkInterfaceDictionary.Add( pingReply.Address, networkInterface );
		}

		public void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			INetworkInterface networkInterface;
			if( _networkInterfaceDictionary.TryGetValue( ipAddress, out networkInterface ) )
			{
				networkInterface.Mac = macAddress;
			}
			Console.WriteLine( ipAddress.ToString() + macAddress.ToString() );
		}
	}
}
