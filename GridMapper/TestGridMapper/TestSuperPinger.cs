using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
using GridMapper;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestSuperPinger
	{
		[Test]
		public void PerformanceTestAsyncPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "127.0.0.1" ) );
			}
			SuperPinger Sp = new SuperPinger( addressToTest );
			Sp.asyncPinger();
		}

		[Test]
		public void PerformanceTestTaskPinger()
		{
			List<IPAddress> addressToTest = new List<IPAddress>();
			for( int i = 0 ; i < 400 ; i++ )
			{
				addressToTest.Add( IPAddress.Parse( "127.0.0.1" ) );
			}
			SuperPinger Sp = new SuperPinger( addressToTest );
			Sp.taskPinger();
		}
		
	}
}
