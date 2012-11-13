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
				IntPtr ptr = GetForegroundWindow();
				int u;
				GetWindowThreadProcessId( ptr, out u );
				Process process = Process.GetProcessById( u );
				if ( process.ProcessName == "cmd" )    //Is the uppermost window a cmd process?
				{
					AttachConsole( process.Id );
					//we have a console to attach to ..
				}
				else
				{
					//no console AND we're in console mode ... create a new console.
					AllocConsole();
				}

				ConsoleMain( startupArgs );
				FreeConsole();
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new Form1() );
			}
			
		}

		static void ConsoleMain( string[] startupArgs )
		{
			Utilities.Arguments args = new Utilities.Arguments( startupArgs );
			Console.Title = "GridMapper";

			Startup StartupOptions = new Startup();
			int maxTasks;

			//help message get priority over everything
			if ( args.IsTrue( "h" ) || args.IsTrue( "?" ) )
			{
				printHelp();
			}
			else
			{
				if ( args.Exists( "t" ) && int.TryParse( args.Single( "t" ), out maxTasks ) )
				{
					if ( maxTasks >= 1 && maxTasks <= 2000 )
					{
						StartupOptions.MaximumTasks = maxTasks;
					}
					else
					{
						Console.WriteLine( "The number of task must be between 1 and 2000, reasonnable limit is around 500" );
						Console.ReadKey( true );
						Environment.Exit( 0 );
					}
				}
				else
				{
					Console.WriteLine( "Invalid argument for -t" );
				}
			}
			Console.ReadKey( true );
		}

		static void printHelp()
		{
			Console.WriteLine( "Usage : gridmapper [-t] [-h|-?]" );
			Console.WriteLine( "" );
			Console.WriteLine( "Options :" );
			Console.WriteLine( "	-h	Print this help message" );
			Console.WriteLine( "	-?	Print this help message" );
			Console.WriteLine( "	-t	Specify the maximum simultanous task running (default is 50)" );
		}

		class Startup
		{
			int _maximumTasks;
			List<IPAddress> _ipToTest;

			public Startup()
			{
				_maximumTasks = 50;
				//_ipToTest = ;
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
