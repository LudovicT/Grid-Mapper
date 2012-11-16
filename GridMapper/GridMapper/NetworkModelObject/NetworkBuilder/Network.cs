using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;

namespace GridMapper.NetworkModelObject
{
	class Network
	{
		static ConcurrentDictionary<IPAddress, IHostEntity> _hostEntities;

		public ConcurrentDictionary<IPAddress, IHostEntity> HostEntities
		{
			get { return _hostEntities; }
		}
			
		public Network()
		{
			_hostEntities = new ConcurrentDictionary<IPAddress, IHostEntity>();
		}

		public void SuperPingerHandling( PingReply pingReply )
		{
			//_hostEntities.GetOrAdd( pingReply.Address, new HostEntity(new Host(pingReply.Address, null ));
		}

		public void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			//Host networkHost;
			//if( _network.NetworkHost.TryGetValue( ipAddress, out networkHost ) )
			//{
			//    INetworkInterface networkInterface;
			//    if( networkHost.NetworkInterfaces.TryGetValue( ipAddress, out networkInterface ) )
			//        networkInterface.Mac = macAddress;
			//}
			//Console.WriteLine( ipAddress.ToString() + macAddress.ToString() );
		}
	}
}
