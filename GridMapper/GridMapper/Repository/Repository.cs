using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Concurrent;

namespace GridMapper.Repository
{
	public class Repository : IRepository
	{
		IDictionary<IPAddress, INetworkDictionaryItem> _networkDictionaryItems;

		public IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems 
		{
			get { return _networkDictionaryItems; } 
		}

		Repository()
		{
			_networkDictionaryItems = new ConcurrentDictionary<IPAddress, INetworkDictionaryItem>();
		}
	}
}
