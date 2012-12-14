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
		IHostEntity HostEntity { get; } //test si il est possible de créer un hostentity
		PingReply PingReply { get; }
		IPAddress IPAddress { get; }
		PhysicalAddress MacAddress { get; }
		IPHostEntry HostEntry { get; }
		IList<PortComputer> Ports { get; }
		IOS OS { get; }

		bool Update( INetworkDictionaryItem networkDictionaryItem );
		bool ChangePort( PortComputer portComputer );
	}
}
