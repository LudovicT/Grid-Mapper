using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Collections.ObjectModel;

namespace GridMapper.NetworkModelObject.INetwork
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
	}

	interface INetworkInterface : INetworkItem
	{
		PhysicalAddress Mac { get; }
	}

	interface IAdministrableSwitch : ISwitch,INetworkInterface
	{

	}

	interface IRouter : INetworkInterface
	{

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