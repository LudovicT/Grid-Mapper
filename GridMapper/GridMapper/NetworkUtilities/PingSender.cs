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
	/// <summary>
	/// PingSender is class that has multiple methods related to the ping feature
	/// It basically sends a ping to a given IP Address with a specified timeout.
	/// </summary>
	public class PingSender
	{
		int _timeout;
		/// <summary>
		/// This is the event method triggered once a ping is completed
		/// </summary>
		public static event EventHandler<PingCompletedEventArgs> PingCompleted;

		public PingSender()
		{
			_timeout = 200;
		}

		public PingSender( Option op )
		{
			_timeout = op.PingTimeout;
		}
		/// <summary>
		/// Calls the ping method and sends the IP Address to be pinged in paramater.
		/// </summary>
		/// <param name="ipAddress">An IP Address to be pinged</param>
		/// <returns></returns>
		public PingReply Ping( IPAddress ipAddress )
		{
			return Ping( ipAddress, _timeout );
		}
		/// <summary>
		/// Same as above, but in the case where the user specified the timeout value.
		/// </summary>
		/// <param name="ipAddress">An IP Address to be pinged</param>
		/// <param name="timeout">An integer value for the timeout</param>
		/// <returns></returns>
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
		/// <summary>
		/// This is an asynchroneous version of the ping feature
		/// </summary>
		/// <param name="ipAddress">An IP Address to be pinged</param>
		public void AsyncPing( IPAddress ipAddress )
		{
			AsyncPing( ipAddress, _timeout );
		}
		/// <summary>
		/// Same as above, but in the case where the user specified a timeout value.
		/// </summary>
		/// <param name="ipAddress">An IP Address to be pinged</param>
		/// <param name="timeout">An integer value for the timeout</param>
		public void AsyncPing( IPAddress ipAddress, int timeout )
		{
			new Ping().SendAsync( ipAddress, timeout, null );
		}
	}
	/// <summary>
	/// The triggered event when a ping is completed.
	/// </summary>
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
