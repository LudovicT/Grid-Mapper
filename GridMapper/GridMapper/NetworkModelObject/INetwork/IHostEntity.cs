using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject
{
	public interface IHostEntity : INetworkItem
	{
		ReadOnlyCollection<IHost> Hosts { get; }
	}
}
