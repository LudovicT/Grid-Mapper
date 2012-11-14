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
		ReadOnlyCollection<INetworkItem> NetworkItems{ get; }
	}

	interface ISwitch : INetworkItem
	{
		HashSet<ICAMdata> CAMTable{ get; }
		ReadOnlyCollection<INetworkItem> NetworkItems { get; }
	}
	interface ICAMdata
	{
		int Port { get; }
		int VLAN { get; }
		PhysicalAddress MacAddress { get; }
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
		int Latency { get; }
	}

	interface IHostEntity
	{
		IServer Server { get; }
		IFirewall Firewall { get; }
		IOS OS { get; }
		IPorts Ports { get; }
		ReadOnlyCollection<IHost> Hosts { get; }
		string NameOS { get; }
		//vérifier si sa position ici est justifié
		string HostName { get; }
	}

	interface INetworkPrinter : IHost
	{
	}

	interface IServer
	{
		//IDhcp Dhcp { get; }
		//IDns Dns { get; }
	}
	interface IFirewall
	{
	}
	interface IOS
	{
	}
	interface IPorts
	{
	}

    interface IPacket
    {
        /*AddressFamily _destinationIp;
        SocketType _datagram;
        ProtocolType _protocol;*/
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