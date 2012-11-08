using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace GridMapper.NetworkModelObject
{
	interface INetworkInterface
	{
		string Description { get; set; }
		string Id { get; set; }
		string Name { get; set; }
		long Speed { get; set; }
		IPAddress Ipv4 { get; set; }
		IPAddress Bridge { get; set; }
		IPAddress Dhcp { get; set; }
		IPAddress Dns { get; set; }
		long AverageLatency { get; set; }
		PhysicalAddress Mac { get; set; }
		//ToDo change for class Port
		bool[] ReceivePort { get; set; }
		bool[] SendPort { get; set; }

	}
}
