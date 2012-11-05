using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NCheckNetworks.NetworkModelObject
{
	public class NetworkInterface : INetworkInterface
	{
		string _description;
		string _id;
		string _mame;
		long _speed;
		IPAddress _ipv4;
		IPAddress _bridge;
		IPAddress _dhcp;
		IPAddress _dns;
		long averageLatence;

		//ToDo change for class Port
		bool[] _receivePort;
		bool[] _sendPort;

		public NetworkInterface()
		{
		}

		public NetworkInterface( IPAddress ipAddress, long latence )
		{
			_ipv4 = ipAddress;
			averageLatence = latence;
		}
	}
}
