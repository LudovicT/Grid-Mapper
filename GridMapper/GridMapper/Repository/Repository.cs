using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;

namespace GridMapper.Repository
{
	public class Repository : IRepository
	{
		ConcurrentDictionary<IPAddress, INetworkDictionaryItem> _networkDictionaryItems;

		public IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems 
		{
			get { return _networkDictionaryItems; } 
		}

		Repository()
		{
			_networkDictionaryItems = new ConcurrentDictionary<IPAddress, INetworkDictionaryItem>();
		}

		//INetworkDictionaryItem Find(IPAddress ipAddress )
		//{
		//    INetworkDictionaryItem networkDictionaryItem;
		//    _networkDictionaryItems.TryGetValue( ipAddress, out networkDictionaryItem );
		//    return networkDictionaryItem;
		//}

		public bool AddOrUpdate( IPAddress ipAddress, PingReply pingReply )
		{
			_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem() );
			return true;
		}
	}
}
