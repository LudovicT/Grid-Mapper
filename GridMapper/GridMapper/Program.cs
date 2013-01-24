using System;
using System.Collections.Generic;
using System.Collections;
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
using GridMapper;


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
		/// Application's main starting point
		/// </summary>
		[STAThread]
		static void Main()
		{
			//FAIL
			Option StartupOptions = new Option();
			List<IPAddress> list = new List<IPAddress>();
			StartupOptions.IpToTest = IPRange.AutoIpRange();


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

				ConsoleProgram.ConsoleMain( startupArgs, StartupOptions );
				FreeConsole();
			}
			else
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				Application.Run( new GridWindow( StartupOptions ) );
			}	
		}
	}
}
