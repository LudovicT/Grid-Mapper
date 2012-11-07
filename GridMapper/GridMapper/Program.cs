using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper
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

			List<IPAddress> addressToTest = new List<IPAddress>();
			addressToTest.Add( IPAddress.Parse( "10.8.97.1" ) );
			addressToTest.Add( IPAddress.Parse( "10.8.98.213" ) );
			addressToTest.Add( IPAddress.Parse( "10.8.103.21" ) );
			addressToTest.Add( IPAddress.Parse( "10.8.104.218" ) );
			addressToTest.Add( IPAddress.Parse( "10.8.105.142" ) );

			SuperPinger Sp = new SuperPinger( addressToTest );
			Sp.taskPinger();


			//As long as there is no -s start arg, the program lauch normally, else it launch itself in silent mode.
			if ( Array.IndexOf( startupArgs, "-s" ) == -1 )
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new Form1() );
			}
			
		}
	}
}
