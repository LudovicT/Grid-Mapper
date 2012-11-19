using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using GridMapper.NetworkModelObject;

namespace GridMapper
{
	public static  partial class NetworkUtilities
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
				Network.HostNameHandler( Dns.GetHostEntry( ipAddress ) );
			} );
			return task;
		}
	}
}
