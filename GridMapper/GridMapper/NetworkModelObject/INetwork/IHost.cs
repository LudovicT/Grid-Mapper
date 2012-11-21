using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.NetworkModelObject
{
	public interface IHost : INetworkInterface
	{
		IPAddress IpAddress { get; }
		int Latency { get; }
	}
}
