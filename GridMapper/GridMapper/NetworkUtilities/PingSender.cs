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

		public static event EventHandler<PingCompletedEventArgs> PingCompleted;

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
			if( pingReply.Address != null )
			{
				//PingCompleted( this, new PingCompletedEventArgs( pingReply ) );
				return pingReply;
			}
			return null;
		}
		public void AsyncPing( IPAddress ipAddress )
		{
			AsyncPing( ipAddress, _timeout );
		}
		public void AsyncPing( IPAddress ipAddress, int timeout )
		{
			new Ping().SendAsync( ipAddress, timeout, null );
		}
	}

	public class PingCompletedEventArgs : EventArgs
	{
		public PingCompletedEventArgs( PingReply pingReply )
		{
			if( pingReply == null ) throw new NullReferenceException();
			PingReply = pingReply;
		}

		public PingReply PingReply { get; private set; }
	}
}
