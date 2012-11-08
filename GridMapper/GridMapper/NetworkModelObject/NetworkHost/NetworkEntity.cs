using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net;

namespace GridMapper.NetworkModelObject
{
	abstract class NetworkEntity
	{
		ConcurrentDictionary<IPAddress,INetworkInterface> _networkInterfaces;

		public ConcurrentDictionary<IPAddress,INetworkInterface> NetworkInterfaces
		{
			get { return _networkInterfaces; }
			set { _networkInterfaces = value; }
		}

		public NetworkEntity()
		{
			_networkInterfaces = new ConcurrentDictionary<IPAddress,INetworkInterface>();
		}
		public NetworkEntity( INetworkInterface networkInterface )
		{
			_networkInterfaces = new ConcurrentDictionary<IPAddress,INetworkInterface>();
			_networkInterfaces.GetOrAdd( networkInterface.Ipv4, networkInterface );
		}
	}
}
