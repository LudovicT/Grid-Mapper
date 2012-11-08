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
		public IPAddress Ipv4
		{
			get { return _ipv4; }
			set { _ipv4 = value; }
		}
		public IPAddress Bridge
		{
			get { return _bridge; }
			set { _bridge = value; }
		}
		public IPAddress Dhcp
		{
			get { return _dhcp; }
			set { _dhcp = value; }
		}
		public IPAddress Dns
		{
			get { return _dns; }
			set { _dns = value; }
		}
		public long AverageLatency
		{
			get { return _averageLatency; }
			set { _averageLatency = value; }
		}
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public long Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}
		public bool[] ReceivePort
		{
			get { return _receivePort; }
			set { _receivePort = value; }
		}
		public bool[] SendPort
		{
			get { return _sendPort; }
			set { _sendPort = value; }
		}

		public NetworkInterface()
		{
		}

		public NetworkInterface( IPAddress ipAddress, long latency )
		{
			_ipv4 = ipAddress;
			_averageLatency = latency;
			_description = string.Empty;
			_id = string.Empty;
			_name = string.Empty;
			_speed = 0;
			_bridge = null;
			_dhcp = null;
			_dns = null;
			_mac = null;
			//ToDo change for class Port
			_receivePort = new bool[65535];
			_sendPort = new bool[65535];
		}
	}
}
