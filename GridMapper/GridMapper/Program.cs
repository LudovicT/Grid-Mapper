using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace GridMapper
{
	static class Program
	{
		[DllImport( "kernel32.dll", SetLastError = true )]
		static extern bool AllocConsole();

		[DllImport( "kernel32.dll", SetLastError = true )]
		static extern bool FreeConsole();

		[DllImport( "kernel32", SetLastError = true )]
		static extern bool AttachConsole( int dwProcessId );

		[DllImport( "user32.dll" )]
		static extern IntPtr GetForegroundWindow();

		[DllImport( "user32.dll", SetLastError = true )]
		static extern uint GetWindowThreadProcessId( IntPtr hWnd, out int lpdwProcessId );

		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string[] startupArgs = Environment.GetCommandLineArgs();
			if ( startupArgs.Length > 1)
			{
				Option StartupOptions = new Option();

				IntPtr ptr = GetForegroundWindow();
				int u;
				GetWindowThreadProcessId( ptr, out u );
				Process process = Process.GetProcessById( u );
				if ( process.ProcessName == "cmd" )    //Is the uppermost window a cmd process?
				{
					StartupOptions.CmdConsole = true;
					AttachConsole( process.Id );
					//we have a console to attach to ..
				}
				else
				{
					//no console AND we're in console mode ... create a new console.
					AllocConsole();
				}

				ConsoleMain( startupArgs, StartupOptions );
				FreeConsole();
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new Form1() );
			}
			
		}

		static void ConsoleMain( string[] startupArgs, Option StartupOptions )
		{
			Arguments args = new Arguments( startupArgs );
			Console.Title = "GridMapper";

			if ( StartupOptions.CmdConsole )
			{
				ConsoleHelper.DeletePath();
			}

			int maxTasks;
			//help message get priority over everything
			if ( args.IsTrue( "h" ) || args.IsTrue( "?" ) || args.IsTrue( "help" ) || 
				 args.Exists( "h" ) || args.Exists( "?" ) || args.Exists( "help" ) )
			{
				ConsoleHelper.PrintHelp();
			}
			else
			{
				if ( ( args.Exists( "t" ) &&	 int.TryParse( args.Single( "t" ), out maxTasks ) ) 
					|| args.Exists( "tasks" ) && int.TryParse( args.Single( "tasks" ), out maxTasks ) )
				{
					if ( maxTasks >= 5 && maxTasks <= 2000 )
					{
						StartupOptions.MaximumTasks = maxTasks;
					}
					else
					{
						Console.WriteLine( "The number of tasks must be between 5 and 2000, reasonnable limit is around 500" );
					}
				}
				else
				{
					Console.WriteLine( "Invalid argument for -t" );
				}
			}


			if ( StartupOptions.CmdConsole )
			{
				//show the path message
				Console.WriteLine( Environment.NewLine );
				Console.Write( Environment.CurrentDirectory.ToString() + ">" );
			}
			else
			{
				Console.WriteLine( Environment.NewLine );
				Console.WriteLine( "Press a key to continue ..." );
				Console.ReadKey( true );
			}
		}

	}
}
