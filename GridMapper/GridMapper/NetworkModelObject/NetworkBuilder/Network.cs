using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;

namespace GridMapper.NetworkModelObject
{
	static class Network
	{
		//va etre utile que lorsqu'on va faire des fichiers ou affiche dans NShape
		static public ConcurrentDictionary<IPAddress, IHostEntity> HostsEntities = new ConcurrentDictionary<IPAddress, IHostEntity>();
		static public ConcurrentDictionary<IPAddress, IHost> Hosts = new ConcurrentDictionary<IPAddress, IHost>();

		//va servir directement pour l'affichage en mode console ou form (plus simple à manipuler
		static public ConcurrentDictionary<IPAddress, PingReply> IPAddresses = new ConcurrentDictionary<IPAddress, PingReply>();
		static public ConcurrentDictionary<IPAddress, PhysicalAddress> MacAddresses = new ConcurrentDictionary<IPAddress, PhysicalAddress>();
		static public ConcurrentDictionary<IPAddress, IPHostEntry> HostsEntries = new ConcurrentDictionary<IPAddress, IPHostEntry>();

		static bool HostsEntitiesIsModified = false;
		static bool HostsIsModified = false;

		static bool IPAddressesIsModified = false;
		static bool MacAddressesIsModified = false;
		static bool HostsEntriesIsModified = false;

		public static event NewIpDetectedEventHandler NewIpDetected;

		public delegate void NewIpDetectedEventHandler( object sender, NewIpDetectedEventArgs e );

		public static void SuperPingerHandling( PingReply pingReply )
		{
			if( IPAddresses.TryAdd( pingReply.Address, pingReply ) )
				IPAddressesIsModified = true;
		}

		public static void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			//si l'ip n'est pas déja présente = true
			if( MacAddresses.TryAdd( ipAddress, macAddress ) )
			{
				//PingReply pingReply;
				//if( IPAddresses.TryGetValue( ipAddress, out pingReply ) )
				//{
				//    Hosts.TryAdd( ipAddress, new Host( ipAddress, macAddress, (int)pingReply.RoundtripTime ) );
				//}
				//else
				//{
				//    Hosts.TryAdd( ipAddress, new Host( ipAddress, macAddress ) );
				//}
			}
		}

		public static void HostNameHandler( IPHostEntry hostEntry )
		{
			foreach( IPAddress ip in hostEntry.AddressList )
			{
				if( !IPAddresses.ContainsKey( ip ) )
					NewIpDetected( hostEntry, new NewIpDetectedEventArgs(ip) ); //probleme lié au sender
				HostsEntries.TryAdd( ip, hostEntry );
			}
		}
	}
	
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
}
