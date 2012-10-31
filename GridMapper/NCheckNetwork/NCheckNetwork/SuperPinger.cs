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
		#region Properties

		//useless ?
		//Ping _ping;

		IList<IPAddress> _ipCollection;
		int _pingCount;

		#endregion //Properties

		#region Fields

		public IList<IPAddress> ipCollection
		{
			get { return _ipCollection; }
			set { _ipCollection = value; }
		}

		#endregion //Fields

		#region Constructors

		public SuperPinger()
			: this ( new List<IPAddress>() )
		{
		}

		public SuperPinger( IList<IPAddress> ipList )
		{
			_ipCollection = ipList;
		}

		#endregion //Constructors

		public void taskPinger()
		{
			foreach( IPAddress ipAddress in _ipCollection )
			{
				Task<List<IPAddress>> task = Task.Factory.StartNew<List<IPAddress>>( () => 
				{
					Task.Factory.FromAsync<List<IPAddress>>(
				} );
			}
		}

		public void asyncPinger()
		{
			foreach( IPAddress ipAddress in _ipCollection )
			{
				Ping pi = new Ping();
				pi.SendAsync( ipAddress, 200, this);
				pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
			}
		}

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
		}
	}
}
