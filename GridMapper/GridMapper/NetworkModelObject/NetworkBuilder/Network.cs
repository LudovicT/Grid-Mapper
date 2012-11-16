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
		static ConcurrentDictionary<IPAddress, IHostEntity> HostsEntities = new ConcurrentDictionary<IPAddress, IHostEntity>();
		static ConcurrentDictionary<IPAddress, PingReply> IPAddresses = new ConcurrentDictionary<IPAddress, PingReply>();
		static ConcurrentDictionary<IPAddress, IHost> Hosts = new ConcurrentDictionary<IPAddress, IHost>();

		//public ConcurrentDictionary<IPAddress, IHostEntity> HostEntities
		//{
		//    get { return _hostEntities; }
		//}

		//public ConcurrentBag<IPAddress> IpAddresses
		//{
		//    get { return _ipAddresses; }
		//}
			
		//public Network()
		//{
		//    if( _hostEntities == null )
		//        _hostEntities = new ConcurrentDictionary<IPAddress, IHostEntity>();
		//    if( _ipAddresses == null )
		//        _ipAddresses = new ConcurrentBag<IPAddress>();
		//}

		public static void SuperPingerHandling( PingReply pingReply )
		{
			IPAddresses.TryAdd( pingReply.Address, pingReply );
		}

		public static void MacAddressHandling( IPAddress ipAddress, PhysicalAddress macAddress )
		{
			PingReply pingReply;
			if( IPAddresses.TryGetValue( ipAddress, out pingReply ) )
				Hosts.TryAdd( ipAddress, new Host( ipAddress, macAddress, (int)pingReply.RoundtripTime ) );
			else
				Hosts.TryAdd( ipAddress, new Host( ipAddress, macAddress ) );
		}

		public static void HostNameHandler( IPHostEntry hostEntry )
		{
			List<IHost> hostsForHostEntity = new List<IHost>();
			foreach( IPAddress ip in hostEntry.AddressList )
			{
				IHost host;
				if( Hosts.TryGetValue( ip,out host ) )
				{
					hostsForHostEntity.Add( host );
				}
			}
			if( hostsForHostEntity.Count > 0 )
			{
				foreach( IPAddress ip in hostsForHostEntity )
				{
					HostsEntities.TryAdd( ip,new HostEntity( hostsForHostEntity,hostEntry.HostName ) );
				}
			}
			else
			{
				//faire the fucking truc pas possible normalement car pas d'ip pour un host existant
			}
		}
	}
}
