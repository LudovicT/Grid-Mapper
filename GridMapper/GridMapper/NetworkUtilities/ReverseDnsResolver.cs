using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using GridMapper.NetworkModelObject;

namespace GridMapper
{
	public class ReverseDnsResolver
	{
		public event EventHandler<HostNameCompletedEventArgs> HostNameCompleted;

		public IPHostEntry GetHostName(IPAddress ipAddress)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry( ipAddress );
			HostNameCompleted( this, new HostNameCompletedEventArgs( ipAddress, hostEntry ) );
			return hostEntry;
		}

		public class HostNameCompletedEventArgs : EventArgs
		{
			IPAddress _ipAddress;
			IPHostEntry _hostEntry;

			public HostNameCompletedEventArgs( IPAddress ipAddress, IPHostEntry hostEntry )
			{
				if( ipAddress == null ) throw new NullReferenceException( "ipAddress" );
				if( hostEntry == null ) throw new NullReferenceException( "hostEntry" );
				_ipAddress = ipAddress;
				_hostEntry = hostEntry;
			}

			public IPHostEntry HostEntry { get { return _hostEntry; } }
			public IPAddress IpAddress { get { return _ipAddress; } }
		}
	}
}
