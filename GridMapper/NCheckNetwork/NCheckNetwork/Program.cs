using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

			//Task.Factory.StartNew( () => );
			SuperPinger Sp = new SuperPinger();
			Sp.asyncPinger();
			if ( Array.IndexOf( startupArgs, "-s" ) != -1 )
			{
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new Form1() );
			
		}
	}
}
