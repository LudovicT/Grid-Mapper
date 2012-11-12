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
			if ( startupArgs.Length > 1 )
			{
				ConsoleMain( startupArgs ); 
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

			// pout -t 1
			Console.WriteLine(args.Single("t") + " "+ args.Exists("t") );
			// pour -h
			Console.WriteLine( args.IsTrue("h"));


			/*
			Startup StartupOptions = new Startup();
			for ( int i = 0; i < startupArgs.Length; i++ )
			{
				switch ( startupArgs[i] )
				{
					case "-t":
						i++;
						if ( i >= startupArgs.ToArray().Count() )
						{

							Console.WriteLine( "Argument manquant" );
							Environment.Exit( 0 );
						}
						else
						{
							int maxTask;
							int.TryParse( startupArgs[i], out maxTask );
							if ( maxTask < 1 || maxTask > 2000 )
							{
								Console.WriteLine( "Entrez un nombre entre 1 et 2000 pour -t\n /!\\ Un nombre supérieur à 1000 peux entrainer une perte de performances" );
								Environment.Exit( 0 );
							}
							StartupOptions.MaximumTasks = maxTask;
						}
						break;
					default:
						break;
				}
			}*/
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
