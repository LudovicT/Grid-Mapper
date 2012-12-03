using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GridMapper.NetworkRepository;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestRepository
	{
		[Test]
		public void TestXmlWriter()
		{
			Repository repo = new Repository();
			PingSender pingSender = new PingSender();
			ARPSender arpSender = new ARPSender();
			Task task1 = Task.Factory.StartNew( () =>
			{
				for( int i = 0 ; i < 10 ; i++ )
				{
					Task task2 = Task.Factory.StartNew( () =>
					{
						repo.AddOrUpdate( IPAddress.Parse( "10.8.99.121" ), pingSender.Ping( IPAddress.Parse( "10.8.99.121" ) ) );
						repo.AddOrUpdate( IPAddress.Parse( "10.8.99.121" ), arpSender.GetMac( IPAddress.Parse( "10.8.99.121" ) ) );
					} );
				}
				Thread.Sleep( 5000 );
			} ).ContinueWith( ( a ) =>
			{
				//pour le moment voir direct dans le dossier
				repo.XmlWriter();
			} );
		}

	}
}
