using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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

	interface IHostEntity
	{
		//IServer Server { get; }
		//IFireWall FireWall { get; }
		//IOS OS { get; }
		//IPorts Ports { get; }
		ReadOnlyCollection<IHost> Hosts { get; }
		string NameOS { get; }
	}

	interface INetworkPrinter : IHost
	{
	}

    interface IPacket
    {
        AddressFamily _destinationIp;
        SocketType _datagram;
        ProtocolType _protocol;
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
	 */
}