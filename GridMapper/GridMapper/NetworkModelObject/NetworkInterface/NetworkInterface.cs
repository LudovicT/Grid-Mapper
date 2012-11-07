using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace GridMapper.NetworkModelObject
{
	public class NetworkInterface : INetworkInterface
	{
		string _description;
		string _id;
		string _name;
		long _speed;
		IPAddress _ipv4;
		IPAddress _bridge;
		IPAddress _dhcp;
		IPAddress _dns;
		long _averageLatency;
		PhysicalAddress _mac;
		//ToDo change for class Port
		bool[] _receivePort;
		bool[] _sendPort;

		public PhysicalAddress Mac 
		{
			get { return _mac; }
			set { _mac = value; }
		}

		public NetworkInterface()
		{
		}

		public NetworkInterface( IPAddress ipAddress, long latency )
		{
			_ipv4 = ipAddress;
			_averageLatency = latency;
		}
	}
}
