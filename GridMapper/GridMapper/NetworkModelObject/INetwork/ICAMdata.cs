using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace GridMapper.NetworkModelObject
{
	interface ICAMdata
	{
		int Port { get; }
		int VLAN { get; }
		PhysicalAddress MacAddress { get; }
		int TTL { get; }
	}
}
