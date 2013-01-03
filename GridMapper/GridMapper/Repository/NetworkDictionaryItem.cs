using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace GridMapper.NetworkRepository
{
	internal class NetworkDictionaryItem : INetworkDictionaryItem
	{
		/* 
		 * ToDo 
		 * WMI's management
		 */
		IHost _host;
		IHostEntity _hostEntity;
		PingReply _pingReply;
		IPAddress _ipAddress;
		PhysicalAddress _macAddress;
		IPHostEntry _hostEntry;
		ConcurrentDictionary<ushort, ushort> _ports;
		IOS _os;

		public IHost Host { get { return _host; } }
		public IHostEntity HostEntity { get { return _hostEntity; } } //test to see if it is possible to create an Host Entitiy
		public PingReply PingReply { get { return _pingReply; } }
		public IPAddress IPAddress { get { return _ipAddress; } }
		public PhysicalAddress MacAddress { get { return _macAddress; } }
		public IPHostEntry HostEntry { get { return _hostEntry; } }
		public ReadOnlyCollection<ushort> Ports { get { return new ReadOnlyCollection<ushort>( _ports.Values.ToList<ushort>() ); } }
		public IOS OS { get { return _os; } }

		public bool Update( INetworkDictionaryItem networkDictionaryItem )
		{
			//peut etre une methode plus mieu
			if( networkDictionaryItem.Host != null ) _host = networkDictionaryItem.Host;
			if( networkDictionaryItem.HostEntity != null ) _hostEntity = networkDictionaryItem.HostEntity;
			if( networkDictionaryItem.PingReply != null ) _pingReply = networkDictionaryItem.PingReply;
			if( networkDictionaryItem.IPAddress != null ) _ipAddress = networkDictionaryItem.IPAddress;
			if( networkDictionaryItem.MacAddress != null ) _macAddress = networkDictionaryItem.MacAddress;
			if( networkDictionaryItem.HostEntry != null ) _hostEntry = networkDictionaryItem.HostEntry;
			if( networkDictionaryItem.OS != null ) _os = networkDictionaryItem.OS;

			return true; //FAIL
		}

		public bool ChangePort( ushort port )
		{
			try
			{
				_ports.GetOrAdd( port, port );
				return true;
			}
			catch
			{
				return false;
			}
		}

		internal NetworkDictionaryItem()
			: this( null, null, null, null, null )
		{
		}

		internal NetworkDictionaryItem(IPAddress ipAddress)
			: this( ipAddress, null, null, null, null )
		{
		}

		internal NetworkDictionaryItem(PingReply pingReply)
			: this( pingReply.Address, pingReply, null, null, null )
		{
		}

		internal NetworkDictionaryItem( IPAddress ipAddress, IPHostEntry hostEntry )
			: this( ipAddress, null, null, hostEntry, null )
		{
		}

		internal NetworkDictionaryItem( IPAddress ipAddress, PhysicalAddress macAddress )
			: this( ipAddress, null, macAddress, null, null )
		{
		}

		internal NetworkDictionaryItem( IPAddress ipAddress, IOS os )
			: this( ipAddress, null, null, null, os )
		{
		}

		internal NetworkDictionaryItem( IPAddress ipAddress, PingReply pingReply, PhysicalAddress macAddress, IPHostEntry hostEntry, IOS os )
		{
			_ipAddress = ipAddress;
			_pingReply = pingReply;
			_macAddress = macAddress;
			_hostEntry = hostEntry;
			_os = os;
			_ports = new ConcurrentDictionary<ushort,ushort>();
		}
	}
}
