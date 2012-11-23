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
				Startup StartupOptions = new Startup();

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
				Application.Run( new GridWindow() );
			}
			
		}

		static void ConsoleMain( string[] startupArgs, Startup StartupOptions )
		{
			Utilities.Arguments args = new Utilities.Arguments( startupArgs );
			Console.Title = "GridMapper";

			if ( StartupOptions.CmdConsole )
			{
				//hide the path message
				string blankSpace = string.Empty;
				for ( int i = 0; i <= Environment.CurrentDirectory.Length; i++ )
				{
					blankSpace += " ";
				}
				Console.WriteLine( "\r" + blankSpace );
			}

			int maxTasks;
			//help message get priority over everything
			if ( args.IsTrue( "h" ) || args.IsTrue( "?" ) ||args.IsTrue( "help" ) )
			{
				printHelp();
			}
			else
			{
				if ( ( args.Exists( "t" ) &&	 int.TryParse( args.Single( "tasks" ), out maxTasks ) ) 
					|| args.Exists( "tasks" ) && int.TryParse( args.Single( "tasks" ), out maxTasks ) )
				{
					if ( maxTasks >= 1 && maxTasks <= 2000 )
					{
						StartupOptions.MaximumTasks = maxTasks;
					}
					else
					{
						Console.WriteLine( "The number of task must be between 1 and 2000, reasonnable limit is around 500" );
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

		static void printHelp()
		{
			Console.WriteLine( "Usage : gridmapper [-h|-?] [-t]" );
			Console.WriteLine( "" );
			Console.WriteLine( "Options :" );
			Console.WriteLine( "	-h,-help	Print this help message" );
			Console.WriteLine( "	-?			Print this help message" );
			Console.WriteLine( "	-t,-tasks	Specify the maximum simultanous task running (default is 50)" );
		}

		class Startup
		{
			bool _cmdConsole;
			int _maximumTasks;
			List<IPAddress> _ipToTest;

			public Startup()
			{
				_cmdConsole = false;
				_maximumTasks = 50;
				//_ipToTest = ;
			}

			public bool CmdConsole
			{
				get
				{
					return _cmdConsole;
				}
				set
				{
					_cmdConsole = value;
				}
			}

			public int MaximumTasks
			{
				get
				{
					return _maximumTasks;
				}
				set
				{
					_maximumTasks = value;
				}
			}

			public List<IPAddress> IpToTest
			{
				get
				{
					return _ipToTest;
				}
				set
				{
					_ipToTest = value;
				}
			}
		}
	}
}
