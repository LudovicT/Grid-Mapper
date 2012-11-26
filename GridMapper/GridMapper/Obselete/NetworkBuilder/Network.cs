using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;

namespace GridMapper.NetworkModelObject
{
	// /!\ OBSELETE
	public static class Network
	{
		#region properties

		//va etre utile que lorsqu'on va faire des fichiers ou affiche dans NShape
		static ConcurrentDictionary<IPAddress, IHostEntity> _hostsEntities = new ConcurrentDictionary<IPAddress, IHostEntity>();
		static ConcurrentDictionary<IPAddress, IHost> _hosts = new ConcurrentDictionary<IPAddress, IHost>();

		//va servir directement pour l'affichage en mode console ou form (plus simple à manipuler
		static ConcurrentDictionary<IPAddress, PingReply> _ipAddresses = new ConcurrentDictionary<IPAddress, PingReply>();
		static ConcurrentDictionary<IPAddress, PhysicalAddress> _macAddresses = new ConcurrentDictionary<IPAddress, PhysicalAddress>();
		static ConcurrentDictionary<IPAddress, IPHostEntry> _hostsEntries = new ConcurrentDictionary<IPAddress, IPHostEntry>();
		static ConcurrentDictionary<IPAddress, bool[]> _portsStatus = new ConcurrentDictionary<IPAddress, bool[]>();

		static bool HostsEntitiesIsModified = false;
		static bool HostsIsModified = false;

		static bool IPAddressesIsModified = false;
		static bool MacAddressesIsModified = false;
		static bool HostsEntriesIsModified = false;

		public static event NewIpDetectedEventHandler NewIpDetected;
		public static event AddIPAddressEventHandler IPAddressAdded;
		public static event AddMacAddressEventHandler MacAddressAdded;
		public static event AddHostEntryEventHandler HostEntryAdded;

		public delegate void NewIpDetectedEventHandler(NewIpDetectedEventArgs e );
		public delegate void AddIPAddressEventHandler(IPAddressAddedEventArgs e );
		public delegate void AddMacAddressEventHandler(MacAddressAddedEventArgs e );
		public delegate void AddHostEntryEventHandler(HostEntryAddedEventArgs e );

		#endregion properties

		#region getter setter

		static public ConcurrentDictionary<IPAddress, IHostEntity> HostsEntities 
		{
			get { return _hostsEntities; }
		}
		static public ConcurrentDictionary<IPAddress, IHost> Hosts 
		{
			get { return _hosts; }
		}
		static public ConcurrentDictionary<IPAddress, PingReply> IPAddresses 
		{
			get { return _ipAddresses; }
		}
		static public ConcurrentDictionary<IPAddress, PhysicalAddress> MacAddresses 
		{
			get { return _macAddresses; }
		}
		static public ConcurrentDictionary<IPAddress, IPHostEntry> HostsEntries
		{
			get { return _hostsEntries; }
		}
		static public ConcurrentDictionary<IPAddress, bool[]> PortsStatus
		{
			get { return _portsStatus; }
		}

		#endregion //getter setter

		#region method

		public static void SuperPingerHandling( PingReply pingReply )
		{
			if( _ipAddresses.TryAdd( pingReply.Address, pingReply ) )
			{
				IPAddressAddedEventArgs e = new IPAddressAddedEventArgs( pingReply );
				if( e != null ) IPAddressAdded( e );
				IPAddressesIsModified = true;
			}
			//peut etre useless
			//else if( _ipAddresses.TryUpdate( pingReply.Address, pingReply, pingReply ) )
			//    IPAddressesIsModified = true;
		}

		public static void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			//si l'ip n'est pas déja présente = true
			if( _macAddresses.TryAdd( ipAddress, macAddress ) )
			{
				MacAddressesIsModified = true;
				MacAddressAddedEventArgs e = new MacAddressAddedEventArgs( ipAddress, macAddress );
				if( e != null ) MacAddressAdded( e );

				//PingReply pingReply;
				//if( IPAddresses.TryGetValue( ipAddress, out pingReply ) )
				//{
				//    Hosts.TryAdd( ipAddress, new Host( ipAddress, macAddress, (int)pingReply.RoundtripTime ) );
				//}
				//else
				//{
				//    Hosts.TryAdd( ipAddres	s, new Host( ipAddress, macAddress ) );
				//}
			}
			else if( _macAddresses.TryUpdate( ipAddress, macAddress, macAddress))
				MacAddressesIsModified = true;
		}

		public static void HostNameHandler( IPHostEntry hostEntry )
		{
			foreach( IPAddress ip in hostEntry.AddressList )
			{
				if( !_ipAddresses.ContainsKey( ip ) )
				{
					NewIpDetectedEventArgs e = new NewIpDetectedEventArgs( ip );
					if( e != null ) NewIpDetected( e ); //probleme lié au sender
				}
				else if( _hostsEntries.TryAdd( ip, hostEntry ) ) //le else pour le moment pour eviter que des infos se perdent par rapport au traitement des nouvelle ip detecté
				{
					HostEntryAddedEventArgs e = new HostEntryAddedEventArgs( ip, hostEntry );
					if( e != null ) HostEntryAdded( e );
					HostsEntriesIsModified = true;
				}
			}
		}

		public static void PortStatusHandler(IPAddress ipAddress, int port, bool status )
		{
			bool[] ports;
			if( _portsStatus.TryGetValue( ipAddress, out ports ) )
			{
				ports[port] = status;
				_portsStatus.TryUpdate( ipAddress, ports, ports );
			}
			else
			{
				ports = new bool[65536];
				ports[port] = status;
				_portsStatus.TryAdd( ipAddress, ports );
			}
		}

		#endregion //method
	}

	#region EventArgs

	public class NewIpDetectedEventArgs : EventArgs
	{
		IPAddress _ipAddress = null;

		public NewIpDetectedEventArgs( IPAddress ipAddress )
		{
			if( ipAddress == null ) throw new NullReferenceException();
			_ipAddress = ipAddress; 
		}

		public IPAddress IpAddress
		{
			get { return _ipAddress; }
		}	
	}

	public class IPAddressAddedEventArgs : EventArgs
	{
		PingReply _pingReply = null;

		public IPAddressAddedEventArgs( PingReply pingReply )
		{
			if( pingReply == null ) throw new NullReferenceException();
			_pingReply = pingReply;
		}

		public PingReply PingReply
		{
			get { return _pingReply; }
		}
	}

	public class MacAddressAddedEventArgs : EventArgs
	{
		IPAddress _ipAddress = null;
		PhysicalAddress _macAddress = null;

		public MacAddressAddedEventArgs( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			if( ipAddress == null ) throw new NullReferenceException( "IPAddress is null" );
			if( macAddress == null ) throw new NullReferenceException("MacAddress is null");
			_ipAddress = ipAddress;
			_macAddress = macAddress;
		}

		public IPAddress IpAddress
		{
			get { return _ipAddress; }
		}

		public PhysicalAddress MacAddress
		{
			get { return _macAddress; }
		}
	}

	public class HostEntryAddedEventArgs : EventArgs
	{
		IPAddress _ipAddress = null;
		IPHostEntry _ipHostEntry = null;

		public HostEntryAddedEventArgs( IPAddress ipAddress, IPHostEntry ipHostEntry )
		{
			if( ipAddress == null ) throw new NullReferenceException( "IPAddress is null" );
			if( ipHostEntry == null ) throw new NullReferenceException( "IPHostEntry is null" );
			_ipAddress = ipAddress;
			_ipHostEntry = ipHostEntry;
		}

		public IPAddress IpAddress
		{
			get { return _ipAddress; }
		}

		public IPHostEntry IPHostEntry
		{
			get { return _ipHostEntry; }
		}
	}

	#endregion //EventArgs
}
