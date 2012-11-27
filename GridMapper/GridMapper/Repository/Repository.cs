using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;

namespace GridMapper.NetworkRepository
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
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( pingReply == null ) throw new ArgumentNullException( "pingReply" );

			//if( !_networkDictionaryItems.ContainsKey( ipAddress ) )
			//    return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
			//INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
			//networkDictionaryItem.Update( new NetworkDictionaryItem( pingReply ) );
			//return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( pingReply ) );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
			}
		}

		public bool AddOrUpdate( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( macAddress == null ) throw new ArgumentNullException( "macAddress" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, macAddress ) );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, macAddress ) );
			}
		}

		public bool AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry)
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( hostEntry == null ) throw new ArgumentNullException( "hostEntry" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, hostEntry ) );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, hostEntry ) );
			}
		}

		public bool AddOrUpdate( IPAddress ipAddress, int indexPort, bool statusPort )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( indexPort > 65535 ) throw new ArgumentOutOfRangeException( "indexPort" );
			
			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( indexPort, statusPort );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				//FAIL
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress ) );
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( indexPort, statusPort );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
		}

		public bool AddOrUpdate( IPAddress ipAddress, IOS os )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( os == null ) throw new ArgumentNullException( "OS" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, os ) );
				return _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				return _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, os ) );
			}
		}

		public ICollection<IPAddress> GetIPAddresses()
		{
			return _networkDictionaryItems.Keys;
		}
	}
}
