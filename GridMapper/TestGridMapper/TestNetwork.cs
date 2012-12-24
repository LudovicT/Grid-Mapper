using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.NetworkInformation;
using System.Net;
using GridMapper.NetworkModelObject;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestNetwork
	{
		[Test]
		public void DeleteARPCache()
		{
			Assert.That( Arping.DeleteCachedARP() == true, "ARP not deleted" );
		}
		[Test]
		public void RetreiveARPCache()
		{
			Assert.That( Arping.GetARPaResult() !=string.Empty );
			Console.WriteLine( Arping.GetARPaResult() );
		}
		[Test]
		public void DeletePingAndRetreiveARPCache()
		{
			Console.WriteLine( "Before Deletion:" );
			Console.WriteLine( Arping.GetARPaResult() );

			Assert.That( Arping.DeleteCachedARP() == true, "ARP not deleted" );

			Console.WriteLine( "After Deletion:" );
			Console.WriteLine( Arping.GetARPaResult() );

			Option Option = new Option();
			Option.IpToTest = IPRange.AutoIpRange();
			PingSender pingSender = new PingSender( Option );
			ThreadPool.SetMinThreads( 200, 200 );
			Parallel.ForEach<int>( Option.IpToTest.Result, new ParallelOptions { MaxDegreeOfParallelism = 200 }, ipInt =>
			{
				IPAddress ip = IPAddress.Parse( ( (uint)ipInt ).ToString() );
				pingSender.AsyncPing( ip );
			} );
			Console.WriteLine( "After Ping" );
			Console.WriteLine( Arping.GetARPaResult() );
		}

		[Test]
		public void ARPCacheTryParse()
		{
			foreach ( var arp in Arping.GetARPaResult().Split( new char[] { '\n', '\r' } ) )
			{
				// Parse out all the MAC / IP Address combinations
				if ( !string.IsNullOrEmpty( arp ) )
				{
					var pieces = ( from piece in arp.Split( new char[] { ' ', '\t' } )
								   where !string.IsNullOrEmpty( piece )
								   select piece ).ToArray();
					if ( pieces.Length == 3  && pieces[2] == "dynamique")
					{
						Console.WriteLine( pieces[0] + " " + pieces[1] + " " + pieces[2]);
					}
				}
			}
		}
	}
}
