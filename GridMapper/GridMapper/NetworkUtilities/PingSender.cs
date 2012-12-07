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

		public event EventHandler<PingCompletedEventArgs> PingCompleted;

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
			PingReply pingReply = new Ping().Send( ipAddress, timeout );
			PingCompleted(this, new PingCompletedEventArgs(pingReply) );
			return pingReply;
		}

		public class PingCompletedEventArgs : EventArgs
		{
			PingReply _pingReply;

			public PingCompletedEventArgs( PingReply pingReply )
			{
				if( pingReply == null ) throw new NullReferenceException();
				_pingReply = pingReply;
			}

			public PingReply PingReply { get { return _pingReply; } }
		}
	}
}
