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
		public IPHostEntry GetHostName(IPAddress ipAddress)
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry( ipAddress );
				return hostEntry;
			}
			catch
			{
				return null;
			}
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
