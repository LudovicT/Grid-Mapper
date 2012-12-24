using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper
{
	public static class Arping
	{
		public static string GetARPaResult()
		{
			Process p = null;
			string output = string.Empty;

			try
			{
				p = Process.Start( new ProcessStartInfo( "arp", "-a" )
				{
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true
				} );

				output = p.StandardOutput.ReadToEnd();

				p.Close();
			}
			catch ( Exception ex )
			{
				throw new Exception( "Error Retrieving 'arp -a' Results", ex );
			}
			finally
			{
				if ( p != null )
				{
					p.Close();
				}
			}

			return output;
		}

		public static bool DeleteCachedARP()
		{
			bool state = true;

			try
			{
				Runcommand( "arp", "-d *" );
			}
			catch ( Exception ex )
			{
				state = false;
				throw new Exception( " Error executing 'arp -d *'", ex );
			}
			return state;
		}

		private static void Runcommand( string filename, string parameter )
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.FileName = filename;
			info.Arguments = parameter;
			info.CreateNoWindow = true;
			info.WindowStyle = ProcessWindowStyle.Hidden;
			Process p = Process.Start( info );
			p.WaitForExit( 5000 );

		}
	}
}
