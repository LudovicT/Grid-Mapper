using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject
{
	interface INetwork
	{
		IList<Host> NetworkHost{ get; set; }
	}
}
