using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NCheckNetworks.NetworkModelObject
{
	interface INetworkInterface
	{
		string _description;
		string _id;
		string _mame;
		long _speed;
		IPAddress _ipv4;
		IPAddress _bridge;
		IPAddress _dhcp;
		IPAddress _dns;

		//ToDo change for class Port
		bool[] _receivePort;
		bool[] _sendPort;


	}
}
