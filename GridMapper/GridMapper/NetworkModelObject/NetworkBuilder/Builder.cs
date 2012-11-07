using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace NCheckNetworks.NetworkModelObject
{
	abstract class Builder : IBuilder
	{
		static protected INetwork _network;
		static protected Dictionary<IPAddress, INetworkInterface> _networkInterfaceDictionary;
	}
}
