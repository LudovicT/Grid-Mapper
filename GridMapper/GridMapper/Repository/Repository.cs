using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

namespace GridMapper.NetworkRepository
{
	public class Repository : IRepository
	{
		ConcurrentDictionary<IPAddress, INetworkDictionaryItem> _networkDictionaryItems;
		bool _isModified;
		bool _isOpen;

		public IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems 
		{
			get { return _networkDictionaryItems; } 
		}

		//a changer par une interface
		public static event EventHandler<RepositoryUpdatedEventArg> OnRepositoryUpdated;

		public Repository()
		{
			_networkDictionaryItems = new ConcurrentDictionary<IPAddress, INetworkDictionaryItem>();
			_isOpen = true;
			_isModified = false;
			Thread th = new Thread( RepositoryUpdateTimer );
			th.Start();
		}

		public void AddOrUpdate( IPAddress ipAddress, PingReply pingReply )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if( pingReply == null ) throw new ArgumentNullException( "pingReply" );
			
			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( pingReply ) );
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				_isModified = _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( pingReply ) );
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
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, macAddress ) );
				_isModified = true;
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
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				_isModified = _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, hostEntry ) );
			}
		}

		//NEED REFACTORING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		public void AddOrUpdate( IPAddress ipAddress, PortComputer portComputer )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			
			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( portComputer );
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				//FAIL
				_networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress ) );
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.ChangePort( portComputer );
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
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
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				_isModified = _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, os ) );
			}
		}

		public ICollection<IPAddress> GetIPAddresses()
		{
			return _networkDictionaryItems.Keys;
		}

		private void RepositoryUpdateTimer()
		{
			while(_isOpen)
			{
				Thread.Sleep(2000);
				if( _isModified )
				{
					_isModified = false;
					OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems ) );
				}
			}
		}

		//version de merde
		public void XmlWriter()
		{
			XmlTextWriter myXmlTextWriter = new XmlTextWriter( "nouveauxlivres.xml", null );
			myXmlTextWriter.Formatting = Formatting.Indented;
			myXmlTextWriter.WriteStartDocument( false );

			myXmlTextWriter.WriteStartElement( "network" );

			foreach( NetworkDictionaryItem item in _networkDictionaryItems.Values )
			{
				myXmlTextWriter.WriteStartElement( "networkitem", null );
				myXmlTextWriter.WriteElementString( "ipaddress", item.IPAddress.ToString() );
				myXmlTextWriter.WriteElementString( "macaddress", item.MacAddress.ToString() );
				//myXmlTextWriter.WriteElementString( "HostName", item.HostEntry.HostName.ToString() );
				myXmlTextWriter.WriteEndElement();
			}

			myXmlTextWriter.WriteEndElement();
			myXmlTextWriter.Flush();
			myXmlTextWriter.Close();
		}

		public void EndThreads()
		{
			_isOpen = false;
		}
	}

	public class RepositoryUpdatedEventArg : EventArgs
	{
		public RepositoryUpdatedEventArg( ConcurrentDictionary<IPAddress, INetworkDictionaryItem> concurrentDictionary )
		{
			if( concurrentDictionary == null ) throw new NullReferenceException();
			ReadOnlyRepository = new ReadOnlyCollection<INetworkDictionaryItem>(concurrentDictionary.Values.ToList<INetworkDictionaryItem>());
		}

		public ReadOnlyCollection<INetworkDictionaryItem> ReadOnlyRepository { get; private set; }
	}
}
