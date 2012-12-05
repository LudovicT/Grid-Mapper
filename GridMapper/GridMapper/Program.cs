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
			//FAIL
			Option StartupOptions = new Option();
			List<IPAddress> list = new List<IPAddress>();
			list.Add(IPAddress.Parse("192.168.1.27"));
			list.Add(IPAddress.Parse("192.168.1.1"));
			StartupOptions.IpToTest = list;


			string[] startupArgs = Environment.GetCommandLineArgs();
			if ( startupArgs.Length > 1)
			{
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
				Application.Run( new GridWindow( StartupOptions ) );
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
				if ( args.Exists("IP"))
				{
					//IPsParser parser = new IPsParser();
					//List<IPAddress> IPsToHandle = parser.IPArgumentsParser(args.Single("IP"));
					//foreach (IPAddress IP in IPsToHandle)
					//{
					//    Console.WriteLine(IP);
					//}
				}
				if (args.Exists("p"))
				{
					List<Int32> PortsToHandle = new List<Int32>(); 
				}
				if( args.Exists( "t" ) || args.Exists( "tasks" ) )
				{
					if ( int.TryParse( args.Single( "t" ), out maxTasks )|| int.TryParse( args.Single( "tasks" ), out maxTasks ) )
					{
						if ( maxTasks >= 5 && maxTasks <= 400 )
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
			}


			if ( StartupOptions.CmdConsole )
			{
				ConsoleHelper.ShowPath();
			}
			else
			{
				ConsoleHelper.EndOfProgram();
			}
		}

	}
}
