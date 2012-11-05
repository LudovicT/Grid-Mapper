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
			addressToTest.Add( IPAddress.Parse( "10.8.111.255" ) );
			addressToTest.Add( IPAddress.Parse( "8.8.8.8" ) );
			addressToTest.Add( IPAddress.Parse( "127.0.0.1" ) );
			addressToTest.Add( IPAddress.Parse( "92.93.94.95" ) );

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
