using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkUtilities;
using System.Threading;
using System.Data;
using System.Diagnostics;
using GridMapper.NetworkRepository;

namespace GridMapper
{
	class ConsoleProgram
	{
		static int OperationLeft = 0;
		static NewExecution _exe;
		static int turningWheelPosition;
		static System.Timers.Timer t = new System.Timers.Timer();

		public static void ConsoleMain( string[] startupArgs, Option StartupOptions )
		{
			Arguments args = new Arguments( startupArgs );
			Console.Title = "GridMapper";


			if ( StartupOptions.CmdConsole )
			{
				ConsoleHelper.DeletePath();
			}

			bool console = StartupOptions.CmdConsole;
			int maxTasks;
			//help message get priority over everything
			if ( args.IsTrue( "h" ) || args.IsTrue( "?" ) || args.IsTrue( "help" ) ||
				 args.Exists( "h" ) || args.Exists( "?" ) || args.Exists( "help" ) )
			{
				ConsoleHelper.PrintHelp();
			}
			else
			{
				Repository.OnRepositoryUpdated += DoNothing; 
				OwnPacketReceiver.EndOfScan += ScanEnded;
				t.Interval = 50;
				t.Elapsed += timer_Tick;
				StartupOptions.CmdConsole = true;
				if ( args.IsTrue( "auto" ) || args.Exists( "auto" ) )
				{
					_exe = new NewExecution( StartupOptions );
					_exe.TaskCompleted += ProgressChanged;
					LaunchScan();
				}
				else
				{
					if ( args.Exists( "IP" ) )
					{
						IPParserResult Ips = IPParser.TryParse( args.Single( "IP" ) );
						if ( Ips.HasError == true )
						{
							Console.WriteLine( "Not a valid input for IP" );
						}
						else
						{
							StartupOptions.IpToTest = Ips;
						}
					}
					//if ( args.Exists( "p" ) )
					//{
					//PortsParserResult PortsToHandle = PortsParser.MainPortsParser(args.Single("p"));
					//IEnumerator enumerator = PortsToHandle.Result.GetEnumerator();
					//}
					//if ( args.Exists( "t" ) || args.Exists( "tasks" ) )
					//{
					//    if ( int.TryParse( args.Single( "t" ), out maxTasks ) || int.TryParse( args.Single( "tasks" ), out maxTasks ) )
					//    {
					//        if ( maxTasks >= 1 && maxTasks <= 2000 )
					//        {
					//            StartupOptions.MaximumTasks = maxTasks;
					//        }
					//        else
					//        {
					//            Console.WriteLine( "The number of tasks must be between 1 and 2000, reasonnable limit is around 4" );
					//        }
					//    }
					//    else
					//    {
					//        Console.WriteLine( "Invalid argument for -t" );
					//    }
					//}
					_exe = new NewExecution( StartupOptions );
					_exe.TaskCompleted += ProgressChanged;
					LaunchScan();
				}

			}



			if ( console )
			{
				ConsoleHelper.ShowPath();
			}
			else
			{
				ConsoleHelper.EndOfProgram();
			}
		}

		private static void LaunchScan()
		{
			OperationLeft = _exe.Option.TotalOperation;
			_exe.StartScan();
			t.Start();
		}

		private static void ProgressChanged( object sender, TaskCompletedEventArgs e )
		{
			if ( e != null )
			{
				if ( OperationLeft >= e.TaskCompleted )
				{
					Interlocked.Add( ref OperationLeft, -e.TaskCompleted );
				}
				else throw new ConstraintException( " wrong number of operation " );
			}
		}

		private static void DoNothing( object o, EventArgs e )
		{
		}

		private static void ScanEnded( object o, EventArgs e )
		{
			Debug.Assert( OperationLeft == 0 );

		}
		private static void timer_Tick( object sender, EventArgs e )
		{
			turningWheelPosition = ( turningWheelPosition + 1 ) % 3;
			int i = 0;
			i = Convert.ToInt32( Math.Round( 100 - (double)OperationLeft / ( _exe.Option.TotalOperation ) * 100 ) );
			string s = string.Empty;
			s = "GridMapper : Scan in progress : [";
			for ( int j = 0; j < 10; j++ )
			{
				if ( j < i % 10 || i == 100)
					s += "-";
				else
					s += " ";
			}
			s += "] " + i + "% ";
			if ( i != 100 )
			{
				switch ( turningWheelPosition )
				{
					case 0:
						s += " /";
						break;
					case 1:
						s += "—";
						break;
					case 2:
						s += " \\";
						break;
				}
			}
			Console.Title = s;
			if ( i == 100 )
			{
				t.Stop();
			}
		}
	}
}
