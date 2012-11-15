﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace GridMapper
{
	static public partial class NetworkUtilities
	{
		static public Task GetHostName( IList<IPAddress> ipCollection )
		{
			Task task = Task.Factory.StartNew( () =>
			{
				foreach( IPAddress ipAddress in ipCollection )
					GetHostName( ipAddress );
			} );
			return task;
		}

		static public Task GetHostName(IPAddress ipAddress)
		{
			Task task = Task.Factory.StartNew( () =>
			{
				IPHostEntry ipHost = Dns.GetHostEntry( ipAddress );
				Console.WriteLine( ipHost.HostName );
			} );
			return task;
		}
	}
}