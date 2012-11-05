using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject.NetworkHost
{
	class HostBase : HostDecorator
	{
		NetworkEntity _networkEntity;

		public HostBase( NetworkEntity networkEntity )
		{
			_networkEntity = networkEntity;
		}
	}
}
