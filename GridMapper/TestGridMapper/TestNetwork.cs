using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using System.Net;
using GridMapper.NetworkModelObject;
using System.Threading;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestNetwork
	{
		[Test]
		public void TestPingCollectionAdd()
		{
			NetworkUtilities.TaskPinger( NetworkUtilities.AutoIpRange(), 120 );
			Thread.Sleep( 5000 );
			Assert.That( Network.IPAddresses.Count >= 1 );
			Network.IPAddresses.Clear();
		}

		[Test]
		public void TestARPCollectionAdd()
		{
			NetworkUtilities.TaskGetMacAddress( NetworkUtilities.AutoIpRange() );
			Thread.Sleep( 5000 );
			Assert.That( Network.MacAddresses.Count >= 1 );
			Network.MacAddresses.Clear();
		}

		[Test]
		public void TestHostNameCollectionAdd()
		{
			NetworkUtilities.GetHostName( NetworkUtilities.AutoIpRange() );
			Thread.Sleep( 5000 );
			Assert.That( Network.HostsEntries.Count >= 1 );
			Network.HostsEntries.Clear();
		}
	}
}
