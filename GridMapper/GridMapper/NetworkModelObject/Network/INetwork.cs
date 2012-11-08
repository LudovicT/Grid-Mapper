using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net;

namespace GridMapper.NetworkModelObject
{
	interface INetwork
	{
		ConcurrentDictionary<IPAddress, Host> NetworkHost { get; set; }
	}
}
