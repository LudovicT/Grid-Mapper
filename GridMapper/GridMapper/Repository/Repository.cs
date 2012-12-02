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

		//a changer par une interface
		public static event RepositoryUpdatedEventHandler OnRepositoryUpdated;
		public delegate void RepositoryUpdatedEventHandler( object sender, RepositoryUpdatedEventArg e );

		public Repository()
		{
			_networkDictionaryItems = new ConcurrentDictionary<IPAddress, INetworkDictionaryItem>();
		}

		public void AddOrUpdate( IPAddress ipAddress, PingReply pingReply )
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
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
			else
			{
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
				OnRepositoryUpdated(this, new RepositoryUpdatedEventArg(_networkDictionaryItems[ipAddress]));
			}
		}

		public void AddOrUpdate( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( macAddress == null ) throw new ArgumentNullException( "macAddress" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, macAddress ) );
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
			else
			{
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, macAddress ) );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
		}

		public void AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( hostEntry == null ) throw new ArgumentNullException( "hostEntry" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, hostEntry ) );
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
			else
			{
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, hostEntry ) );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
		}

		public void AddOrUpdate( IPAddress ipAddress, int indexPort, bool statusPort )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( indexPort > 65535 ) throw new ArgumentOutOfRangeException( "indexPort" );
			
			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( indexPort, statusPort );
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
			else
			{
				//FAIL
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress ) );
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( indexPort, statusPort );
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
		}

		public void AddOrUpdate( IPAddress ipAddress, IOS os )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( os == null ) throw new ArgumentNullException( "OS" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress, os ) );
				_networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
			else
			{
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, os ) );
				OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems[ipAddress] ) );
			}
		}

		public ICollection<IPAddress> GetIPAddresses()
		{
			return _networkDictionaryItems.Keys;
		}
	}

	public class RepositoryUpdatedEventArg
	{
		private INetworkDictionaryItem _networkDictionaryItem = null;

		public RepositoryUpdatedEventArg( INetworkDictionaryItem networkDictionaryItem )
		{
			if( networkDictionaryItem == null ) throw new NullReferenceException();
			_networkDictionaryItem = networkDictionaryItem;
		}

		public INetworkDictionaryItem NetworkDictionaryItem { get { return _networkDictionaryItem; } }
	}
}
