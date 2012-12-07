using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GridMapper
{
	public class IPParserResult
	{
		public IPParserResult()
		{
		}
		internal IPParserResult( string errorMessage, IEnumerable<IPAddressV4Range> result )
		{
			//Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}

		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public IEnumerable<IPAddressV4Range> Result { get; private set; }
	}

	

	public enum Token
	{
		Unknow,
		ExcludeUnknow,
		WhiteSpace,
		New,
		Range,
		CIDR,
		Exclude,
		ExcludeIP,
		ExcludeRange,
		ExcludeCIDR,
		End,
	}

	public class IPRangeList : IEnumerable<IPAddressV4Range>
	{
		public readonly List<IPAddressV4Range> storage = new List<IPAddressV4Range>();
		public void Add( IPAddressV4 ip )
		{
			storage.Add(new IPAddressV4Range(ip,ip));
		}

		public void Add( IPAddressV4 from, IPAddressV4 to )
		{
			storage.Add( new IPAddressV4Range( from, to ) );
		}

		public void Remove( IPAddressV4 ip )
		{
			storage.RemoveAll( IPV4Range => 
			{
				return ( IPV4Range.From.Address == ip.Address && IPV4Range.To.Address == ip.Address );
			});
		}

		public void Remove( IPAddressV4 from, IPAddressV4 to )
		{
			storage.RemoveAll( IPV4Range =>
			{
				return ( IPV4Range.From.Address == from.Address && IPV4Range.To.Address == to.Address );
			} );
		}

		public IEnumerator<IPAddressV4Range> GetEnumerator()
		{
			foreach ( IPAddressV4Range item in storage )
			{
				yield return item;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class GetRange
	{
		public IEnumerable<IPAddressV4> GetIPRange( int startIP, int endIP )
		{
			uint sIP = (uint)startIP;
			uint eIP = (uint)endIP;
			IPAddressV4 IPV4 = new IPAddressV4();
			while (sIP <= eIP)
			{
				IPV4.Address = (int)sIP;
				yield return IPV4;
				sIP++;
			}
		}
	}

	public class IPAddressV4Range
	{
		public IPAddressV4 From { get; private set; }
		public IPAddressV4 To { get; private set; }
			
		public IPAddressV4Range( IPAddressV4 ipFrom, IPAddressV4 ipTo )
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
			IPRangeList result = new IPRangeList();
			IPAddressV4 currentIP;
			IPAddressV4 IPRange;
			string errorMessage = null;
			Token token;
			while ((token = parser.NextToken()) != Token.End )
			{
				if ( token == Token.Exclude )
				{
					switch ( parser.NextToken( out currentIP, out IPRange ) )
					{
						case Token.New:
						case Token.End:
							result.Remove( currentIP );
							break;
						case Token.Range:
						case Token.CIDR:
							result.Remove( currentIP, IPRange );
							break;
						case Token.ExcludeIP:
							result.Remove( currentIP );
							break;
						case Token.ExcludeRange:
						case Token.ExcludeCIDR:
							result.Remove( currentIP, IPRange );
							break;
						case Token.Unknow:
						case Token.ExcludeUnknow:
							errorMessage = "Invalid Format";
							break;
					}
				}
				else
				{
					switch ( parser.NextToken( out currentIP, out IPRange ) )
					{
						case Token.New:
						case Token.End:
							result.Add( currentIP );
							break;
						case Token.Range:
						case Token.CIDR:
							result.Add( currentIP, IPRange );
							break;
						case Token.ExcludeIP:
							result.Remove( currentIP );
							break;
						case Token.ExcludeRange:
						case Token.ExcludeCIDR:
							result.Remove( currentIP, IPRange );
							break;
						case Token.Unknow:
						case Token.ExcludeUnknow:
							errorMessage = "Invalid Format";
							break;
					}
				}
			}
			return new IPParserResult(errorMessage, result);
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
				if ( Current == ' ' )
				{
					while ( !IsEndOfInput && Current == ' ' ) _pos++;
					return true;
				}
				else
				{
					return ++_pos < _s.Length;
				}
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }


			public Token NextToken( )
			{
				if ( !IsEndOfInput )
				{
					switch ( Current )
					{
						case ',':
							Forward();
							return Token.New;
						case '-':
							Forward();
							return Token.Range;
						case '/':
							Forward();
							return Token.CIDR;
						case '!':
							Forward();
							return Token.Exclude;
						case ' ':
							Forward();
							return Token.WhiteSpace;
						default:
							return Token.Unknow;
					}
				}
				return Token.End;
			}

			public Token NextToken(out IPAddressV4 ip1, out IPAddressV4 ip2)
			{
				ip1 = new IPAddressV4();
				ip2 = new IPAddressV4();
				if ( !IsEndOfInput )
				{
					if ( IsIPAddressV4Short( out ip1 ) )
					{
						switch ( NextToken() )
						{
							case Token.New:
								return Token.New;
							case Token.Range:
								if ( IsIPAddressV4Short( out ip2, 255 ) )
								{
									return Token.Range;
								}
								break;
							case Token.CIDR:
								//todo
								return Token.CIDR;
							case Token.End:
								return Token.End;
							default:
								return Token.Unknow;
						}
					}
					switch ( NextToken() )
					{
						case Token.Exclude:
							if ( IsIPAddressV4Short( out ip1 ) )
							{
								switch ( NextToken() )
								{
									case Token.New:
										return Token.ExcludeIP;
									case Token.Range:
										if ( IsIPAddressV4Short( out ip2, 255 ) )
										{
											return Token.ExcludeRange;
										}
										break;
									case Token.CIDR:
										//todo
										return Token.ExcludeCIDR;
									case Token.End:
										return Token.End;
									default:
										return Token.Exclude;
								}
							}
							return Token.ExcludeUnknow;
						case Token.End:
							return Token.End;
						default:
							return Token.Unknow;

					}
				}
				return Token.End;
			}



			public bool IPAddressV4Range(IPAddressV4 ipFrom, out IPAddressV4Range r )
			{
				r = null;
				if ( MatchChar( '-' ) )
				{
					IPAddressV4 ipTo;
					if ( IsIPAddressV4Short( out ipTo, 255 ) )
					{
						r = new IPAddressV4Range( ipFrom, ipTo );
						return true;
					}
					return false;
				}
				return false;
			}

			public bool IsNewIPAddressV4 { get { return MatchChar( ',' );}}

			public bool IsIPAddressV4Full( out IPAddressV4 ip )
			{
				ip = new IPAddressV4();
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
					ip.B0 = b1;
					ip.B1 = b2;
					ip.B2 = b3;
					ip.B3 = b4;
					return true;
				}
				return false;
			}

			public bool IsIPAddressV4Short( out IPAddressV4 ip, byte autoFillValue = 0 )
			{
				ip = new IPAddressV4();
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
					ip.B0 = b1;
					ip.B1 = b2;
					ip.B2 = b3;
					ip.B3 = b4;
					return true;
				}
				return false;
			}

			private bool MatchChar( char c )
			{
				if ( !IsEndOfInput && Current == ' ' )
				{
					Forward();
				}
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
				if ( Current == ' ' )
				{
					Forward();
				}
				while (!IsEndOfInput && Char.IsDigit( Current ))
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