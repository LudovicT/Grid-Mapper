using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace GridMapper
{
	public class IPParserResult
	{
		internal IPParserResult( string errorMessage, IEnumerable<IPAddressRange> result )
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}

		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public IEnumerable<IPAddressRange> Result { get; private set; }
	}

	public class IPAddressRange
	{
		public IPAddress From { get; private set; }
		public IPAddress To { get; private set; }
			
		public IPAddressRange( IPAddress ipFrom, IPAddress ipTo )
		{
			From = ipFrom;
			To = ipTo;
		}

	}

	public static class IPParser
	{
		static public IPParserResult TryParse( string rangeOfIP )
		{
			Parser parser = new Parser(rangeOfIP);
			List<IPAddressRange> Result = new List<IPAddressRange>();
			IPAddress CurrentIP;
			IPAddressRange IPRange;
			string errorMessage = string.Empty;

			while ( parser.IsIPAddressShort( out CurrentIP ) )
			{
				if(parser.IsNewIPAddress)
				{
					Result.Add( new IPAddressRange( CurrentIP, CurrentIP ) );
				}
				else if ( parser.IsIPAddressRange( CurrentIP, out IPRange ) )
				{
					Result.Add( IPRange );
				}
				else
				{
					Result.Add( new IPAddressRange( CurrentIP, CurrentIP ) );
				}
			}
			return new IPParserResult(errorMessage, Result);
		}

		class Parser
		{
			readonly string _s;
			int _pos;


			public Parser( string s )
			{
				_s = s;
			}

			char Current { get { return _s[_pos]; } }

			bool Forward()
			{
				// Skip whitespaces...
				while ( MatchChar( ' ' ) ){}
				return ++_pos < _s.Length;
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			public bool IsIPAddressRange(IPAddress ipFrom, out IPAddressRange r )
			{
				r = null;
				if ( MatchChar( '-' ) )
				{
					IPAddress ipTo = null;
					if ( IsIPAddressShort( out ipTo, 255 ) )
					{
						r = new IPAddressRange( ipFrom, ipTo );
						return true;
					}
					return false;
				}
				return false;
			}

			public bool IsNewIPAddress { get { return MatchChar( ',' );}}

			public bool IsIPAddressFull( out IPAddress ip )
			{
				ip = null;
				byte b1;
				byte b2;
				byte b3;
				byte b4;
				if ( IsByte( out b1 )
					&& MatchChar( '.' )
					&& IsByte( out b2 )
					&& MatchChar( '.' )
					&& IsByte( out b3 )
					&& MatchChar( '.' )
					&& IsByte( out b4 ) )
				{
					ip = new IPAddress( new byte[] { b1, b2, b3, b4 } );
					return true;
				}
				return false;
			}

			public bool IsIPAddressShort( out IPAddress ip, byte autoFillValue = 0 )
			{
				ip = null;
				byte b1;
				byte b2 = autoFillValue;
				byte b3 = autoFillValue;
				byte b4 = autoFillValue;
				if ( IsByte( out b1 ) )
				{
					if ( MatchChar( '.' ) && IsByte( out b2 ) )
					{
						if ( MatchChar( '.' ) && IsByte( out b3 ) )
						{
							if ( MatchChar( '.' ) && IsByte( out b4 ) )
							{
							}
						}
					}
					ip = new IPAddress( new byte[] { b1, b2, b3, b4 } );
					return true;
				}
				return false;
			}

			private bool MatchChar( char c )
			{
				if ( !IsEndOfInput && Current == c )
				{
					Forward();
					return true;
				}
				return false;
			}

			private bool IsByte( out byte b )
			{
				b = 0;
				string returnedByte = "";
				while ( Char.IsDigit( Current ) )
				{
					returnedByte += Current;
					Forward();
				}

				if ( returnedByte != String.Empty )
				{
					if ( byte.TryParse( returnedByte, out b ) )
					{
						return true;
					}
					return false;
				}
				return false;
			}

		}

	}
}