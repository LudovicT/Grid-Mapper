using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject.NetworkHost
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
