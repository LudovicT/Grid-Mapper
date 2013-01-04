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
	public interface INetworkDictionaryItem
	{
		IHost Host { get; }
		IHostEntity HostEntity { get; } //test to see if it is possible to create an Host Entitiy
		PingReply PingReply { get; }
		IPAddress IPAddress { get; }
		PhysicalAddress MacAddress { get; }
		IPHostEntry HostEntry { get; }
		ReadOnlyCollection<ushort> Ports { get; }
		IOS OS { get; }

		bool Update( INetworkDictionaryItem networkDictionaryItem );
		bool ChangePort( ushort port );
	}
}
