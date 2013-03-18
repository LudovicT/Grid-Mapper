using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GridMapper.NetworkModelObject
{
	public interface IHost : INetworkInterface
	{
		IPAddressV4 IpAddressV4 { get; }
	}
}
