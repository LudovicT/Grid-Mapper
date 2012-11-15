using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;

namespace GridMapper.NetworkModelObject
{
	class AutoBuilder : Builder
	{
		ConcurrentDictionary<IPAddress,IHostEntity> _hostEntities;
		
		public AutoBuilder()
		{
			_hostEntities = new ConcurrentDictionary<IPAddress,IHostEntity>();
		}

		public void SuperPingerHandling( PingReply pingReply )
		{
			//test if exist interface with this ip
			//NetworkInterface networkInterface = new NetworkInterface(pingReply.Address, pingReply.RoundtripTime);
			//Host host = new Host( networkInterface );
			//_network.NetworkHost.GetOrAdd(pingReply.Address, host );
		}

		public void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
		//    Host networkHost;
		//    if( _network.NetworkHost.TryGetValue( ipAddress, out networkHost ) )
		//    {
		//        INetworkInterface networkInterface;
		//        if( networkHost.NetworkInterfaces.TryGetValue( ipAddress, out networkInterface ) )
		//            networkInterface.Mac = macAddress;
		//    }
		//    Console.WriteLine( ipAddress.ToString() + macAddress.ToString() );
		}
	}
}
