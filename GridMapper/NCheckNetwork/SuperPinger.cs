﻿using System;
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

		#region Constructor

		public SuperPinger()
			: this ( new List<IPAddress>() )
		{
		}

		public SuperPinger( IList<IPAddress> ipList )
		{
			_ipCollection = ipList;
			_pingCount = 0;
		}

		#endregion //Constructor

		public void taskPinger()
		{
			Task<List<IPAddress>> task = Task.Factory.StartNew<List<IPAddress>>( () =>
			{
				List<IPAddress> retreivedAdress = new List<IPAddress>();

				asyncPinger();
				/*foreach ( IPAddress ipAddress in _ipCollection )
				{
					Ping pi = new Ping();
					pi.SendAsync( ipAddress, 200, this );
					pi.PingCompleted += new PingCompletedEventHandler( ( object sender, PingCompletedEventArgs e ) =>
					{
						_pingCount++;
						if ( e.Reply.Status == IPStatus.Success )
						{
							retreivedAdress.Add( e.Reply.Address );
							MessageBox.Show( e.Reply.Address.ToString() + " " + e.Reply.RoundtripTime.ToString() );
						}
						else
						{
							MessageBox.Show( e.Reply.Status.ToString() );
						}

					} );
				}*/
				return retreivedAdress;
			} );
		}

		public void asyncPinger()
		{
			foreach( IPAddress ipAddress in _ipCollection )
			{
				ping( ipAddress );
			}
		}

		public void ping( IPAddress ipAddress )
		{
			Ping pi = new Ping();
			pi.SendAsync( ipAddress, 200, this );
			pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
		}

		void asyncPingComplete( object sender, PingCompletedEventArgs e )
		{
			_pingCount++;

			PingReply pingReply = e.Reply;
			//((AutoResetEvent)e.UserState).Set();
			if( pingReply.Status == IPStatus.Success )
			{
				MessageBox.Show( "Address: " + pingReply.Address.ToString() + " pingCount : " + _pingCount + "\n" + "Roundtrip time: " + pingReply.RoundtripTime.ToString() + "\nStatus :" + pingReply.Status.ToString());
			}

		}
	}
}
