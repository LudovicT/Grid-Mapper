using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Collections.ObjectModel;
using System.Net;

namespace GridMapper.NetworkModelObject
{
	interface INetworkItem
	{

	}

	interface IHub : INetworkItem
	{
		ReadOnlyCollection<INetworkItem> NetworkItems{get;}
	}

	interface ISwitch : INetworkItem
	{
		struct CAMdata
		{
			int _port;
			int _VLAN;
			PhysicalAddress _mac;
			int _TTL;
		}
		HashSet<CAMdata> CAMTable{ get; }
		ReadOnlyCollection<INetworkItem> NetworkItems { get; }
	}

	interface CAMdata
	{
		int Port { get; }
		int VLAN { get; }
		PhysicalAddress Mac { get; }
		int TTL { get; }
	}

	interface INetworkInterface : INetworkItem
	{
		PhysicalAddress Mac { get; }
	}

	interface IAdministrableSwitch : ISwitch,IHost
	{

	}

	interface IHost : INetworkInterface
	{
		IPAddress IpAddress { get; }
	}
	/*interface IEthernetDevice
	{

	}
	 * interface IBridge
	{

	}
	 * interface IWifiDevice
	{

	}
	 * interface IFireWall
	{

	}
	 * interface IServer
	{

	}
	 * interface Server
	{

	}
	 * interface Host
	{

	}
	 */
}