using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace NCheckNetworks
{
	public class SuperPinger
	{
		Ping _ping;
		//IPAddressCollection _ipCollection;
		IList<IPAddress> _ipCollection;
		int _pingCount;

		public /*IPAddressCollection*/ IList<IPAddress> ipCollection
		{
			get { return _ipCollection; }
			set { _ipCollection = value; }
		}

		public SuperPinger()
		{
			_ping = new Ping();
			_ipCollection = new List<IPAddress>();
			_ipCollection.Add( IPAddress.Parse( "8.8.8.8" ) );
			_ipCollection.Add( IPAddress.Parse( "127.0.0.1" ) );
			_ipCollection.Add( IPAddress.Parse( "10.8.111.255" ) );
			_ipCollection.Add( IPAddress.Parse( "8.8.8.8" ) );
			
		}
		public void taskPinger()
		{
			foreach( IPAddress ipAddress in _ipCollection )
			{
				Task<List<IPAddress>> task = Task.Factory.StartNew
			}
			
		}

		/*public void asyncPinger()
		{
			foreach( IPAddress ipAddress in _ipCollection )
			{
				Ping pi = new Ping();
				pi.SendAsync( ipAddress, 200, this);
				pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
			}
		}*/

		void asyncPingComplete( object sender, PingCompletedEventArgs e )
		{
			_pingCount++;

			PingReply pingReply = e.Reply;
			//((AutoResetEvent)e.UserState).Set();
			if( pingReply.Status == IPStatus.Success )
			{
				MessageBox.Show( "Address: " + pingReply.Address.ToString() + " pingCount : " + _pingCount );
				MessageBox.Show( "Roundtrip time: " + pingReply.RoundtripTime.ToString() );
			}
			//throw new Exception("The method or operation is not implemented.");
		}
	}
}
