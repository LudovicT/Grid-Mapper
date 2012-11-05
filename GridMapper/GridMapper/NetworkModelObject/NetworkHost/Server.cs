using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject.NetworkHost
{
	class Server : HostDecorator
	{
		NetworkEntity _networkEntity;

		public Server( NetworkEntity networkEntity )
		{
			_networkEntity = networkEntity;
		}
	}
}
