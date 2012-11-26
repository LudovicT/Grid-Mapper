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

		public Repository()
		{
			_networkDictionaryItems = new ConcurrentDictionary<IPAddress, INetworkDictionaryItem>();
		}

		public bool AddOrUpdate( IPAddress ipAddress, PingReply pingReply )
		{
			if( !_networkDictionaryItems.ContainsKey( ipAddress ) )
				return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
			INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
			networkDictionaryItem.Update( new NetworkDictionaryItem( pingReply ) );
			return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );

			//if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			//{
			//    INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
			//    networkDictionaryItem.update( new NetworkDictionaryItem( pingReply ) );
			//    return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			//}
			//else
			//{
			//    return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
			//}
		}
	}
}
