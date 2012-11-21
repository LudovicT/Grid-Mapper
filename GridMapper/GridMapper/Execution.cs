using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using GridMapper.NetworkModelObject;

namespace GridMapper
{
	class Execution
	{
		public void startScan(Startup StartupOptions )
		{
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			CancellationToken token = tokenSource.Token;
			Task task = Task.Factory.StartNew( () =>
				{
					PingSender.TaskPinger( StartupOptions.IpToTest, StartupOptions.PingTimeout );
				} )
				.ContinueWith( result => 
					{
						if ( Network.IPAddresses.Count > 0 )
						{
							foreach ( IPAddress IP in Network.IPAddresses.Keys )
							{
								ARPSender.TaskGetMacAddress( IP );
							}
						}
						else
						{
							tokenSource.Cancel();
						}
					});
		}
	}
}
