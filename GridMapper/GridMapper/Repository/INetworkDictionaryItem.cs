using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net;

namespace GridMapper.Repository
{
	public interface INetworkDictionaryItem
	{
		IHost Host { get; }
		IHostEntity HostEntity { get; } //test si il est possible de créer un hostentity
		PingReply PingReply { get; }
		IPAddress IPAddress { get; }
		PhysicalAddress MacAddress { get; }
		IPHostEntry HostEntry { get; }
		bool[] PortStatus { get; }

		//juste des idées
		bool Update( INetworkDictionaryItem networkDictionaryItem );//on lui passe l'objet a update 
	}
}
