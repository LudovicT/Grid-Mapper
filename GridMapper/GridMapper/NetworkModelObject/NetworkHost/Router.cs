using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject.NetworkHost
{
	class Router : HostDecorator
	{
		NetworkEntity _networkEntity;

		public Router( NetworkEntity networkEntity )
		{
			_networkEntity = networkEntity;
		}
	}
} 
