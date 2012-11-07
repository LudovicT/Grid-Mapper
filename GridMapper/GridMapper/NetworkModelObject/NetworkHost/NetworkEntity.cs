using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject
{
	abstract class NetworkEntity
	{
		IList<INetworkInterface> _networkInterfaces;

		public NetworkEntity()
		{
			_networkInterfaces = new List<INetworkInterface>();
		}
		public NetworkEntity( INetworkInterface networkInterface )
		{
			_networkInterfaces = new List<INetworkInterface>();
			_networkInterfaces.Add( networkInterface );
		}
	}
}
