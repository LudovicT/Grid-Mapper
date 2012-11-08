using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Concurrent;

namespace GridMapper.NetworkModelObject
{
	class Network : INetwork
	{
		ConcurrentDictionary<IPAddress,Host> _networkHosts;
		IPAddress _ipv4;
		IPAddress _ipv6;
		int _mtu;

		public ConcurrentDictionary<IPAddress, Host> NetworkHost
		{
			get { return _networkHosts; }
            set { _networkHosts = value; }
		}

		public Network()
		{
			_networkHosts = new ConcurrentDictionary<IPAddress,Host>();
			_ipv4 = null;
			_ipv6 = null;
			_mtu = 0;
		}
	}
}
