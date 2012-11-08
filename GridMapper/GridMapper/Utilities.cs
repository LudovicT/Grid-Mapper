using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using GridMapper.NetworkModelObject;
using System.Windows.Forms;

namespace GridMapper
{
	public class Utilities
	{
		#region ARP

		[DllImport( "iphlpapi.dll", ExactSpelling = true )]
		public static extern int SendARP( int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen );

		public static void taskGetMacAddress( List<IPAddress> Ips)
		{
			//Task.Factory.StartNew( () => 	Parallel.ForEach<IPAddress>( Ips, Ip => new AutoBuilder().MacAddressHandling( Ip, getMacAddress( Ip ) ) ) );
			
			Task[] Tasks = new Task[Ips.Count];
			for(int i = 0; i < Ips.Count; i++)
			{
				int value = i;
				Tasks[value] = Task.Factory.StartNew( () =>
				{
					new AutoBuilder().MacAddressHandling( Ips[value], getMacAddress( Ips[value] ) );
				} );
			}
		}

		public static PhysicalAddress getMacAddress(IPAddress dst)
		{
			int intAddress = BitConverter.ToInt32( dst.GetAddressBytes(), 0 );
			byte[] macAddr = new byte[6];
			uint macAddrLen = (uint)macAddr.Length;
			if ( SendARP( intAddress, 0, macAddr, ref macAddrLen ) != 0 )
			{
				//throw new InvalidOperationException( "SendARP failed." );
			}

			string[] str = new string[(int)macAddrLen];
			for ( int i = 0; i < macAddrLen; i++ )
				str[i] = macAddr[i].ToString( "x2" );
			string fullMac = string.Join( "", str ).ToUpper();
			return PhysicalAddress.Parse(fullMac);
		}

		#endregion //ARP

		#region Ping

		static public void TaskPinger( IList<IPAddress> ipCollection )
		{
			Task task = Task.Factory.StartNew( () =>
			{
				ListPinger( ipCollection );
			} );
		}

		static public void ListPinger( IList<IPAddress> ipCollection )
		{
			foreach( IPAddress ipAddress in ipCollection )
			{
				Ping( ipAddress );
			}
		}

		static public void Ping( IPAddress ipAddress )
		{
			Ping pi = new Ping();
			pi.SendAsync( ipAddress, 200, pi );
			pi.PingCompleted += new PingCompletedEventHandler( asyncPingComplete );
		}

		static void asyncPingComplete( object sender, PingCompletedEventArgs e )
		{
			AutoBuilder autoBuilder = new AutoBuilder();
			if( e.Reply.Status == IPStatus.Success )
			{
				autoBuilder.SuperPingerHandling( e.Reply );
			}
		}

		#endregion //Ping
	}
}
