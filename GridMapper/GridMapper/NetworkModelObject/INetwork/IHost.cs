using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.NetworkModelObject
{
	interface IHost : INetworkInterface
	{
		IPAddress IpAddress { get; }
		int Latency { get; }
	}
}
