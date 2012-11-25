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
				return Dns.GetHostEntry( ipAddress );
		}
	}
}
