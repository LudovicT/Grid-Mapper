using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

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
					NetworkUtilities.TaskPinger( StartupOptions.IpToTest );
				} ).ContinueWith( () => 
					{
						if ( Network.IPAddresses.count > 0 )
						{
							foreach ( IPAddress IP in Network.IPAddresses.Keys )
							{
								NetworkUtilities.TaskGetMacAddress( IP );
							}
						}
						else
						{
							tokenSource.Cancel();
						}
					}, token);
		}
	}
}
