using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;

namespace NCheckNetworks
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string[] startupArgs = Environment.GetCommandLineArgs();

			AsyncComplexLocalPing();
			if ( Array.IndexOf( startupArgs, "-s" ) != -1 )
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new Form1() );
			}
			
		}

		public static void AsyncComplexLocalPing()
		{
			// Get an object that will block the main thread.
			AutoResetEvent waiter = new AutoResetEvent( false );

			// Ping's the local machine.
			Ping pingSender = new Ping();

			// When the PingCompleted event is raised, 
			// the PingCompletedCallback method is called.
			pingSender.PingCompleted += new PingCompletedEventHandler( AsyncPingCompleted );

			IPAddress address = IPAddress.Parse("10.8.111.255");

			// Create a buffer of 32 bytes of data to be transmitted. 
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes( data );

			// Wait 10 seconds for a reply. 
			int timeout = 1000;

			// Set options for transmission: 
			// The data can go through 64 gateways or routers 
			// before it is destroyed, and the data packet 
			// cannot be fragmented.
			PingOptions options = new PingOptions( 64, true );

			// Send the ping asynchronously. 
			// Use the waiter as the user token. 
			// When the callback completes, it can wake up this thread.
			pingSender.SendAsync( address, timeout, buffer, options, waiter );

			// Prevent this example application from ending. 
			// A real application should do something useful 
			// when possible.
			waiter.WaitOne();
			waiter.WaitAll(  );
			Console.WriteLine( "Ping example completed." );
		}
		private static void AsyncPingCompleted( Object sender, PingCompletedEventArgs e )
		{
			System.Net.NetworkInformation.PingReply reply = e.Reply;
			((System.Threading.AutoResetEvent)e.UserState).Set();
			if( reply.Status == System.Net.NetworkInformation.IPStatus.Success )
			{
				MessageBox.Show( "Address: " + reply.Address.ToString() );
				MessageBox.Show( "Roundtrip time: " + reply.RoundtripTime.ToString() );
			}
		}
	}
}
