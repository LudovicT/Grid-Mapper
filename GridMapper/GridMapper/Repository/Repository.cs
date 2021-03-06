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
using System.Collections.ObjectModel;
using System.Threading;

namespace GridMapper.NetworkRepository
{
	public class Repository : IRepository
	{
		ConcurrentDictionary<IPAddress, INetworkDictionaryItem> _networkDictionaryItems;
		bool _isModified;
		bool _isOpen;
		System.Timers.Timer timer = new System.Timers.Timer();

		public IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems 
		{
			get { return _networkDictionaryItems; } 
		}

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

		public void AddOrUpdate( IPAddress ipAddress)
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );

			if( _networkDictionaryItems.ContainsKey( ipAddress ) )
			{
				INetworkDictionaryItem networkDictionaryItem = _networkDictionaryItems[ipAddress];
				networkDictionaryItem.Update( new NetworkDictionaryItem( ipAddress ) );
				_isModified = _networkDictionaryItems.TryUpdate( ipAddress, networkDictionaryItem, networkDictionaryItem );
			}
			else
			{
				_isModified = _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress ) );
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
				_isModified = _networkDictionaryItems.TryAdd( ipAddress, new NetworkDictionaryItem( ipAddress, macAddress ) );
			}
		}

		public void AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry )
		{
			if( ipAddress == null ) throw new ArgumentNullException( "ipAddress" );
			if ( hostEntry == null )
			{
				hostEntry = new IPHostEntry();
				hostEntry.HostName = ipAddress.ToString();
			}

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
		public void AddOrUpdate( IPAddress ipAddress, ushort portComputer )
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
			timer.Interval = 200;
			timer.Enabled = true;
			timer.Elapsed += RepositoryUpdate;
			timer.Start();
		}

		private void RepositoryUpdate( object sender, EventArgs e )
		{
			if ( _isOpen )
			{
				if ( _isModified )
				{
					_isModified = false;
					OnRepositoryUpdated( this, new RepositoryUpdatedEventArg( _networkDictionaryItems ) );
				}
			}
			else
			{
				timer.Stop();
			}
		}

		//shitty version...
		public void XmlWriter(Stream stream)
		{
			XmlTextWriter myXmlTextWriter = new XmlTextWriter( stream, null );
			myXmlTextWriter.Formatting = Formatting.Indented;
			myXmlTextWriter.WriteStartDocument( false );

			myXmlTextWriter.WriteStartElement( "network" );

			foreach( NetworkDictionaryItem item in _networkDictionaryItems.Values )
			{
				myXmlTextWriter.WriteStartElement( "Networkitem", null );
				myXmlTextWriter.WriteElementString( "IPaddress", item.IPAddress.ToString() );
				if ( item.MacAddress != null && item.MacAddress != PhysicalAddress.None )
				{
					myXmlTextWriter.WriteElementString( "Macaddress", item.MacAddress.ToString() );
				}
				if ( item.HostEntry != null && item.HostEntry.HostName != null && item.HostEntry.HostName != string.Empty )
				{
					myXmlTextWriter.WriteElementString( "HostName", item.HostEntry.HostName.ToString() );
				}
				string ports = string.Empty;
				foreach ( ushort port in item.Ports )
				{
					if ( ports != string.Empty )
					{
						ports += ",";
					}
					ports += port.ToString();
				}
				if ( ports != string.Empty )
				{
					myXmlTextWriter.WriteElementString( "Ports", ports );
				}
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
