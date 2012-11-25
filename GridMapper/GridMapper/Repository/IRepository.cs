using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.Repository
{
	public interface IRepository
	{
		IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems { get; }

	}
}
