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
	public static class PingSender
	{
		static public Task TaskPinger( IList<IPAddress> ipCollection, int timeout )
		{
			//start a new task and stock it, we only start one task because the ping method SendAsync is already asyncronous
			Task task = Task.Factory.StartNew( () =>
			{
				ListPinger( ipCollection, timeout );
			} );
			return task;
		}

		public static void ListPinger( IList<IPAddress> ipCollection, int timeout )
		{
			foreach( IPAddress ipAddress in ipCollection )
			{
				Ping( ipAddress, timeout );
			}
		}

		public static void Ping( IPAddress ipAddress, int timeout )
		{
			Ping pi = new Ping();
			pi.SendAsync( ipAddress, timeout, pi );
			pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
		}

		static void asyncPingComplete( object sender, PingCompletedEventArgs e )
		{
			if( e.Reply.Status == IPStatus.Success )
			{
				Network.SuperPingerHandling( e.Reply );
			}
		}
	}
}
