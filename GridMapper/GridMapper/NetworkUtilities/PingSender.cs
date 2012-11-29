using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;

namespace GridMapper
{
	public class PingSender
	{
		int _timeout;

		public PingSender()
		{
			_timeout = 200;
		}

		public PingSender( Option op )
		{
			_timeout = op.PingTimeout;
		}

		public PingReply Ping( IPAddress ipAddress )
		{
			return Ping( ipAddress, _timeout );
		}
		public PingReply Ping( IPAddress ipAddress, int timeout )
		{
			return new Ping().Send( ipAddress, timeout );
		}
	}
}
