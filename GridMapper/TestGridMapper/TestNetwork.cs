using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using System.Net;
using GridMapper.NetworkModelObject;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestNetwork
	{
		[Test]
		public void TestPingCollection()
		{
			NetworkUtilities.Ping( IPAddress.Parse( "127.0.0.1" ), 120 );

			Assert.That( Network.IPAddresses.Count == 1 );
			Assert.That( Network.IPAddresses[0].Address == IPAddress.Parse( "127.0.0.1" ) );
		}
	}
}
