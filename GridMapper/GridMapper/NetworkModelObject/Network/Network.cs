using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NCheckNetworks.NetworkModelObject
{
	class Network : INetwork
	{
		IList<Host> _networkHosts;
		IPAddress _ipv4;
		IPAddress _ipv6;
		int _mtu;

		public IList<Host> NetworkHost
		{
			get { return _networkHosts; }
            set { _networkHosts = value; }
		}

		public Network()
		{
			_networkHosts = new List<Host>();
			_ipv4 = null;
			_ipv6 = null;
			_mtu = 0;
		}
	}
}
