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
	public static partial class NetworkUtilities
	{
		public static Task TaskPinger( IList<IPAddress> ipCollection )
		{
			//start a new task and stock it, we only start one task because the ping method SendAsync is already asyncronous
			Task task = Task.Factory.StartNew( () =>
			{
				ListPinger( ipCollection );
			} );
			return task;
		}

		public static void ListPinger( IList<IPAddress> ipCollection )
		{
			foreach( IPAddress ipAddress in ipCollection )
			{
				Ping( ipAddress );
			}
		}

		public static void Ping( IPAddress ipAddress )
		{
			Ping pi = new Ping();
			pi.SendAsync( ipAddress, 200, pi );
			pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
		}

		static void asyncPingComplete( object sender, PingCompletedEventArgs e )
		{
			if( e.Reply.Status == IPStatus.Success )
			{
				//_autoBuilder.SuperPingerHandling( e.Reply );
			}
		}
	}
}
