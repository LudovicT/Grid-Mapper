using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace NCheckNetworks.NetworkModelObject
{
	interface INetworkInterface
	{
		PhysicalAddress Mac { get; set; }


	}
}
