using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;
using System.IO;

namespace GridMapper.NetworkRepository
{
	public interface IRepository
	{
		IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems { get; }

		//needs to be changed by interfaces
		//public event RepositoryUpdatedEventHandler OnRepositoryUpdated;
		//public delegate void RepositoryUpdatedEventHandler( object sender, RepositoryUpdatedEventArg e );

		void AddOrUpdate( IPAddress ipAddress, PingReply pingReply );
		void AddOrUpdate( IPAddress ipAddress, PhysicalAddress macAddress );
		void AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry );
		void AddOrUpdate( IPAddress ipAddress, ushort port );
		void AddOrUpdate( IPAddress ipAddress, IOS os );
		void AddOrUpdate( IPAddress ipAddress );
		ICollection<IPAddress> GetIPAddresses();
		void EndThreads();
		void XmlWriter(Stream stream);
	}

	
}
