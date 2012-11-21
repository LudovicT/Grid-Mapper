using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace GridMapper.NetworkModelObject
{
	public interface INetworkInterface : INetworkItem
	{
		PhysicalAddress Mac { get; }
	}
}
