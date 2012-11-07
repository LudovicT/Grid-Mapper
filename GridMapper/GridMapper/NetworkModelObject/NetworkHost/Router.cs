using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject.NetworkHost
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
