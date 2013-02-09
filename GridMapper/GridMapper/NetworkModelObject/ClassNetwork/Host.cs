using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

namespace GridMapper.NetworkModelObject
{
	class Host : IHost
	{
		IPAddress _ipAddress;
		PhysicalAddress _macAddress;
		int _latency;

		#region Interfaces Membres

		public IPAddress IpAddress
		{
			get { return _ipAddress; }
		}

		public PhysicalAddress Mac
		{
			get { return _macAddress; }
		}

		public int Latency
		{
			get { return _latency; }
		}

		#endregion //Interfaces Membres

		public Host( IPAddress ipAddress, PhysicalAddress macAddress )
			: this(ipAddress, macAddress, 0)
		{
		}

		public Host( IPAddress ipAddress, PhysicalAddress macAddress, int latency )
		{
			_macAddress = macAddress;
			_ipAddress = ipAddress;
			_latency = latency;
		}

		public void Describe( TextWriter output )
		{
			output.WriteLine( "Physical Address : {0}", _macAddress );
			output.WriteLine( "IP Address : {0}", _ipAddress );
		}
	}
}
