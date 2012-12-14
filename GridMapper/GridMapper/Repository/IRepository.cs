using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using GridMapper.NetworkModelObject;

namespace GridMapper.NetworkRepository
{
	public interface IRepository
	{
		IDictionary<IPAddress, INetworkDictionaryItem> NetworkDictionaryItems { get; }

		//a changer par des interface
		//public event RepositoryUpdatedEventHandler OnRepositoryUpdated;
		//public delegate void RepositoryUpdatedEventHandler( object sender, RepositoryUpdatedEventArg e );

		void AddOrUpdate( IPAddress ipAddress, PingReply pingReply );
		void AddOrUpdate( IPAddress ipAddress, PhysicalAddress macAddress );
		void AddOrUpdate( IPAddress ipAddress, IPHostEntry hostEntry );
		void AddOrUpdate( IPAddress ipAddress, PortComputer portComputer );
		void AddOrUpdate( IPAddress ipAddress, IOS os );
		ICollection<IPAddress> GetIPAddresses();


	}

	
}
