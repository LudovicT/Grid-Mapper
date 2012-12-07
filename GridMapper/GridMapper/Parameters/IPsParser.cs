using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;

namespace GridMapper
{
	public class IPParserResult
	{
		public IPParserResult()
		{
		}
		internal IPParserResult( string errorMessage, IEnumerable<int> result )
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}

		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public IEnumerable<int> Result { get; private set; }
	}

	[StructLayout( LayoutKind.Explicit )]
	public struct IPAddressV4
	{
		[FieldOffset( 0 )]
		public byte B0;
		[FieldOffset( 1 )]
		public byte B1;
		[FieldOffset( 2 )]
		public byte B2;
		[FieldOffset( 3 )]
		public byte B3;
		[FieldOffset( 0 )]
		public int Address;

		public override string ToString()
		{
			return String.Format( "{0}.{1}.{2}.{3}", B0, B1, B2, B3 );
		}

		public override bool Equals( object obj )
		{
			if ( obj is IPAddressV4 )
			{
				return Address == ( (IPAddressV4)obj ).Address;
			}
			if ( obj is int )
			{
				return Address == (int)obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Address;
		}
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

	public class IPRangeList : IEnumerable<int>
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
			//storage.RemoveAll( IPV4Range => 
			//{
			//    return ( IPV4Range.From.Address == ip.Address && IPV4Range.To.Address == ip.Address );
			//});
		}

		public void Remove( IPAddressV4 from, IPAddressV4 to )
		{
			//storage.RemoveAll( IPV4Range =>
			//{
			//    return ( IPV4Range.From.Address == from.Address && IPV4Range.To.Address == to.Address );
			//} );
		}


		public IEnumerator<int> GetEnumerator()
		{
			foreach ( IPAddressV4Range IPRange in storage )
			{
				for ( int i = IPRange.From.Address; i <= IPRange.To.Address; i++ )
				{
					yield return i;
				}
			}
		}


		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
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
			while ( parser.NextToken() != Token.End )
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
						return new IPParserResult( errorMessage, null );
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
								if ( NextToken() == Token.Exclude )
								{
									return Token.Exclude;
								}
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
					if ( BitConverter.IsLittleEndian )
					{
						ip.B0 = b4;
						ip.B1 = b3;
						ip.B2 = b2;
						ip.B3 = b1;
					}
					else
					{
						ip.B0 = b1;
						ip.B1 = b2;
						ip.B2 = b3;
						ip.B3 = b4;
					}
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
					if ( BitConverter.IsLittleEndian )
					{
						ip.B0 = b4;
						ip.B1 = b3;
						ip.B2 = b2;
						ip.B3 = b1;
					}
					else
					{
						ip.B0 = b1;
						ip.B1 = b2;
						ip.B2 = b3;
						ip.B3 = b4;
					}
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