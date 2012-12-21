using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;
using System.Net;

namespace GridMapper.NetworkRepository
{
	public interface INetworkDictionaryItem
	{
		IHost Host { get; }
		IHostEntity HostEntity { get; } //test to see if it is possible to create an Host Entitiy
		PingReply PingReply { get; }
		IPAddress IPAddress { get; }
		PhysicalAddress MacAddress { get; }
		IPHostEntry HostEntry { get; }
		IList<ushort> Ports { get; }
		IOS OS { get; }

		bool Update( INetworkDictionaryItem networkDictionaryItem );
		bool ChangePort( ushort port );
	}
}
