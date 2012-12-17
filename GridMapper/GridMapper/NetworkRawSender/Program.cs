using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ConsoleApplication5
{
	class Program
	{
		static void usage()
		{
			Console.WriteLine( "Usage: Executable_file_name [-as source-addr] [-ad dest-addr] [-ps source-port]" );
			Console.WriteLine( "                  [-pd dest-port] [-x payload-size] [-n send-count] [-b bind-addr]" );
			Console.WriteLine( " Options" );
			Console.WriteLine( "     -as source-addr     Source address for IP packet" );
			Console.WriteLine( "     -ad dest-addr       Destination address for IP packet" );
			Console.WriteLine( "     -ps source-port     Source port for UDP packet" );
			Console.WriteLine( "     -pd dest-port       Destination port for UDP packet" );
			Console.WriteLine( "     -b  bind-addr       Local address to bind raw socket to" );
			Console.WriteLine( "     -x  payload-size    Number of bytes for UDP payload" );
			Console.WriteLine( "     -n  send-count      Number of times to send packet" );
			Console.WriteLine( "....Else default values will be used..." );
		}
		static void Main( string[] args )
		{
			System.Diagnostics.Process proc;
			RawSocketPing pingSocket = null;
			AddressFamily addressFamily = AddressFamily.Unspecified;
			string remoteHost = "localhost";
			IPAddress remoteAddress = null;
			int dataSize = 15000, ttlValue = 128, sendCount = 100000;

			usage();
			Console.WriteLine();

			// Parse the command line
			for( int i = 0 ; i < args.Length ; i++ )
			{
				try
				{
					if( (args[i][0] == '-') || (args[i][0] == '/') )
					{
						switch( Char.ToLower( args[i][1] ) )
						{
							case 'a':
								// Force to ping over IPv4 or IPv6
								int af = System.Convert.ToInt32( args[++i].ToString() );

								if( af == 4 )
								{
									addressFamily = AddressFamily.InterNetwork;
								}
								else if( af == 6 )
								{
									addressFamily = AddressFamily.InterNetworkV6;
								}
								else
								{
									usage();
									return;
								}
								break;
							case 'i':
								// What TTL value to set on ping echo request?
								ttlValue = System.Convert.ToInt32( args[++i].ToString() );
								break;
							case 'l':
								// How many bytes in the ping echo request payload?
								dataSize = System.Convert.ToInt32( args[++i].ToString() );
								break;
							case 'n':
								// How many times to send an echo request
								sendCount = System.Convert.ToInt32( args[++i].ToString() );
								break;
							default:
								usage();
								return;
						}
					}
					else
					{
						// If argument doesn't start with a '-' or '/' assume its the
						//    hostname we're to ping
						remoteHost = args[i];
					}
				}
				catch
				{
					usage();
					return;
				}
			}

			// Try to resolve the address or name passed
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry( remoteHost );

				if( hostEntry.AddressList.Length == 0 )
				{
					Console.WriteLine( "Ping request could not fine host {0}. Please check the name and try again", remoteHost );
					return;
				}

				if( addressFamily == AddressFamily.Unspecified )
				{
					// No preference was specified so take first address
					remoteAddress = hostEntry.AddressList[0];
				}
				else
				{
					// Try to find a resolved address that matches the user selection
					foreach( IPAddress addr in hostEntry.AddressList )
					{
						remoteAddress = addr;

						if( remoteAddress.AddressFamily == addressFamily )
						remoteAddress = null;
					}
					if( remoteAddress == null )
					{
						Console.WriteLine( "Ping request could not fine host {0}. Please check the name and try again", remoteHost );
						return;
					}
				}
			}
			catch( SocketException err )
			{
				Console.WriteLine( "Bad name {0}: {1}", remoteHost, err.Message );
				return;
			}

			// Get our process ID which we'll use in the ICMP ID field
			proc = System.Diagnostics.Process.GetCurrentProcess();

			try
			{
				// Create a RawSocketPing class that wraps all the ping functionality
				pingSocket = new RawSocketPing( remoteAddress.AddressFamily, ttlValue, dataSize, sendCount, (ushort)proc.Id );

				Console.WriteLine( "Starting from Main()..." );
				Console.WriteLine();
				// Set the destination address we want to ping
				Console.WriteLine( "Setting the destination address..." );
				pingSocket.PingAddress = IPAddress.Parse("10.8.110.194");
				Console.WriteLine();
				// Initialize the raw socket
				Console.WriteLine( "Initializing the raw socket..." );
				pingSocket.InitializeSocket();
				Console.WriteLine();
				// Create the ICMP packets to send
				Console.WriteLine( "Creating the ICMP packets to send..." );
				pingSocket.BuildPingPacket();
				Console.WriteLine();
				// Actually send the ping request and wait for response
				Console.WriteLine( "Sending the ping request and wait for response..." );
				pingSocket.DoPing();
			}
			catch( SocketException err )
			{
				Console.WriteLine( "Socket error occurred: {0}", err.Message );
			}
			finally
			{
				if( pingSocket != null )
					pingSocket.Close();
			}
			return;
		}
	}
}
