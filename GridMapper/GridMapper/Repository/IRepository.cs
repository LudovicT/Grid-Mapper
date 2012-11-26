using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;

namespace GridMapper.Repository
{
	public interface IRepository
	{
		IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems { get; }

		bool AddOrUpdate( IPAddress ipAddress, PingReply pingReply );
		bool AddOrUpdate( IPAddress ipAddress, PhysicalAddress macAddress );
		bool AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry);
		bool AddOrUpdate( IPAddress ipAddress, int indexPort, bool statusPort );
		bool AddOrUpdate( IPAddress ipAddress, IOS os );
		ICollection<IPAddress> GetIPAddresses();
	}
}