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
	}
}
