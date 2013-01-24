using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper
{
	public static class ConsoleHelper
	{
		public static void PrintHelp()
		{
			Console.WriteLine( "Usage : gridmapper [-h|-?] [-t]" );
			Console.WriteLine( "" );
			Console.WriteLine( "Options :" );
			Console.WriteLine( "	-h,-help	Print this help message" );
			Console.WriteLine( "	-?			Print this help message" );
			Console.WriteLine( "	-t,-tasks	Specify the maximum simultanous task running (default is 50)" );
			Console.WriteLine( "    -p,-ports   Specify the ports to be scanned (between 1 and 65535)" );
		}
		public static void DeletePath()
		{
			//hide the path message
			string blankSpace = string.Empty;
			for ( int i = 0; i <= Environment.CurrentDirectory.Length; i++ )
			{
				blankSpace += " ";
			}
			Console.WriteLine( "\r" + blankSpace );
		}

		public static void ShowPath()
		{
			Console.WriteLine( Environment.NewLine );
			Console.Write( Environment.CurrentDirectory.ToString() + ">" );
		}
		
		public static void EndOfProgram()
		{
			Console.WriteLine( Environment.NewLine );
			Console.ReadKey( true );
		}
		//public static void PrintResult(object sender, PrintResultEventsArgs e)
		//{
		//    //
		//}
	}
}
