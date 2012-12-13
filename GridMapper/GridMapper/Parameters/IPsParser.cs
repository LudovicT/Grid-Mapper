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

	public enum Token
	{
		Unknown,
		ExcludeUnknown,
		WhiteSpace,
		New,
		Range,
		CIDR,
		End,
		Exclude,
		ExcludeIP,
		ExcludeRange,
		ExcludeCIDR,
		ExcludeEnd,
	}

	public class IPRangeList : IEnumerable<int>
	{
		public readonly List<IPAddressV4Range> storage = new List<IPAddressV4Range>();

		public void Add( IPAddressV4 ip )
		{
			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						IPAddressV4Range IPV4Range = storage[i];
						if ( IsBetween( ip, IPV4Range.From, IPV4Range.To ) ) return;
						if ( IsNextOfFrom( ip, IPV4Range.From, IPV4Range.To ) || IsNextOfFrom( ip, IPV4Range.From))
						{
							Remove( IPV4Range.From, IPV4Range.To );
							Add( ip, IPV4Range.To );
						}
						if ( IsNextOfTo( ip, IPV4Range.From, IPV4Range.To ) || IsNextOfTo( ip, IPV4Range.To))
						{
							Remove( IPV4Range.From, IPV4Range.To );
							Add( IPV4Range.From, ip );
						}
					}
				} while ( storage.Count != count );
			}
			storage.Add(new IPAddressV4Range(ip,ip));
			storage.Sort((IPAddressV4Range a, IPAddressV4Range b) => a.From.Address.CompareTo(b.From.Address));
		}

		public void Add( IPAddressV4 from, IPAddressV4 to )
		{
			bool added = false;
			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						IPAddressV4Range IPV4Range = storage[i];
						//already in the storage
						// Disposition
						// -----------------		IPV4Range
						//      --------			from, to
						if ( IsBetween( from, IPV4Range.From, IPV4Range.To ) && IsBetween( to, IPV4Range.From, IPV4Range.To ) ) return;

						// Disposition
						// ------------				IPV4Range
						//      --------------		from, to
						if ( IsBetween( from, IPV4Range.From, IPV4Range.To ) && !IsBetween( to, IPV4Range.From, IPV4Range.To ) )
						{
							Remove( IPV4Range.From, IPV4Range.To );
							storage.Add( new IPAddressV4Range( IPV4Range.From, to ) );
							added = true;
						}

						// Disposition
						//      --------------		IPV4Range
						// ------------				from, to
						if ( !IsBetween( from, IPV4Range.From, IPV4Range.To ) && IsBetween( to, IPV4Range.From, IPV4Range.To ) )
						{
							Remove( IPV4Range.From, IPV4Range.To );
							storage.Add( new IPAddressV4Range( from, IPV4Range.To ) );
							added = true;
						}
						if ( IsNextOfFrom( to, IPV4Range.From, IPV4Range.To ) || IsNextOfFrom( to, IPV4Range.From ) )
						{
							Remove( IPV4Range.From, IPV4Range.To );
							Add( from, IPV4Range.To );
							added = true;
						}
						if ( IsNextOfTo( from, IPV4Range.From, IPV4Range.To ) || IsNextOfTo( from, IPV4Range.To ) )
						{
							Remove( IPV4Range.From, IPV4Range.To );
							Add( IPV4Range.From, to );
							added = true;
						}
					}
				} while ( storage.Count != count );
			}
			if ( !added )
			{
				storage.Add( new IPAddressV4Range( from, to ) );
			}
			storage.Sort( ( IPAddressV4Range a, IPAddressV4Range b ) => a.From.Address.CompareTo( b.From.Address ) );
		}

		public void Remove( IPAddressV4 ip )
		{
			IPAddressV4 tmpFrom;
			IPAddressV4 tmpTo;

			//exclusive border of the exclusion
			tmpFrom = ip;
			tmpTo = ip;
			tmpFrom.Address--;
			tmpTo.Address++;

			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						IPAddressV4Range IPV4Range = storage[i];
						if ( IsBetween( ip, IPV4Range.From, IPV4Range.To ) )
						{
							Remove( IPV4Range.From, IPV4Range.To );
							//check if the range is not a single IP
							if ( IPV4Range.From.Address != IPV4Range.To.Address )
							{
								Add( IPV4Range.From, tmpFrom );
								Add( tmpTo, IPV4Range.To );
							}
						}
					}
				} while ( storage.Count != count );
			}
		}

		public void Remove( IPAddressV4 from, IPAddressV4 to )
		{
			IPAddressV4 tmpFrom;
			IPAddressV4 tmpTo;

			//for the exclusive exlusion
			tmpFrom = from;
			tmpTo = to;
			tmpFrom.Address--;
			tmpTo.Address++;

			if ( storage.Count > 0 )
			{
				int count;
				do
				{
					count = storage.Count;
					for ( int i = 0; i < storage.Count; i++ )
					{
						IPAddressV4Range IPV4Range = storage[i];
						// Disposition
						// -----------------		IPV4Range
						//      --------			from, to
						if ( IsBetween( from, IPV4Range.From, IPV4Range.To ) && IsBetween( to, IPV4Range.From, IPV4Range.To ) )
						{
							storage.Remove( IPV4Range );
							//if not a full range deletion then add the remaining
							if ( IPV4Range.From.Address != from.Address && IPV4Range.To.Address != to.Address )
							{
								Add( IPV4Range.From, tmpFrom );
								Add( tmpTo, IPV4Range.To );
							}
							//inclusive left
							if ( IPV4Range.From.Address == from.Address && IPV4Range.To.Address != to.Address )
							{
								Add( tmpTo, IPV4Range.To );
							}
							//inclusive right
							if ( IPV4Range.From.Address != from.Address && IPV4Range.To.Address == to.Address )
							{
								Add( IPV4Range.From, tmpFrom );
							}
						}

						// Disposition
						// ------------				IPV4Range
						//      --------------		from, to
						if ( IsBetween( from, IPV4Range.From, IPV4Range.To ) && !IsBetween( to, IPV4Range.From, IPV4Range.To ) )
						{
							storage.Remove( IPV4Range );
							Add( IPV4Range.From, tmpFrom );
						}

						// Disposition
						//      --------------		IPV4Range
						// ------------				from, to
						if ( !IsBetween( from, IPV4Range.From, IPV4Range.To ) && IsBetween( to, IPV4Range.From, IPV4Range.To ) )
						{
							storage.Remove( IPV4Range );
							Add( tmpTo, IPV4Range.To );
						}


						// Disposition
						//		----				IPV4Range
						//   ---------------		from, to
						if ( IsBetween( IPV4Range.From, from, to ) && IsBetween( IPV4Range.To, from, to ) )
						{
							storage.Remove( IPV4Range );
						}
					}
				} while ( storage.Count != count );
			}
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
		/// <summary>
		/// Return a bool depending if search is between from and to
		/// </summary>
		/// <param name="search">the search</param>
		/// <param name="from">the left side of the range</param>
		/// <param name="to">the right side of the range</param>
		/// <returns>true if search is between from and to</returns>
		bool IsBetween( IPAddressV4 search, IPAddressV4 from, IPAddressV4 to )
		{
			if ( from.Address > to.Address )
			{
				return from.Address >= search.Address && search.Address >= to.Address;
			}
			return from.Address <= search.Address && search.Address <= to.Address;
		}

		// single ip range
		bool IsNextOfFrom( IPAddressV4 search, IPAddressV4 from, IPAddressV4 to )
		{
			return search.Address + 1 == from.Address && search.Address + 1 == to.Address;
		}
		// range of ip
		bool IsNextOfFrom( IPAddressV4 search, IPAddressV4 from)
		{
			return search.Address + 1 == from.Address;
		}

		// single ip range
		bool IsNextOfTo( IPAddressV4 search, IPAddressV4 from, IPAddressV4 to )
		{
			return search.Address - 1 == from.Address && search.Address - 1 == to.Address;
		}
		//range of ip
		bool IsNextOfTo( IPAddressV4 search, IPAddressV4 to )
		{
			return search.Address - 1 == to.Address;
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
			while ( parser.IsNotEndOfInput )
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
					case Token.ExcludeEnd:
						result.Remove( currentIP );
						break;
					case Token.ExcludeRange:
					case Token.ExcludeCIDR:
						result.Remove( currentIP, IPRange );
						break;
					case Token.Unknown:
					case Token.ExcludeUnknown:
						errorMessage = "Invalid format for IP Arguments";
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
				return ++_pos < _s.Length;
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			public bool IsNotEndOfInput { get { return !IsEndOfInput; } }

			public Token NextToken( )
			{
				if ( IsNotEndOfInput )
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
							return Token.Unknown;
					}
				}
				return Token.End;
			}

			public Token NextToken(out IPAddressV4 ip1, out IPAddressV4 ip2)
			{
				ip1 = new IPAddressV4();
				ip2 = new IPAddressV4();
				int CIDR = 0;
				Token token;
				if ( IsNotEndOfInput )
				{
					if ( Current != '!' )
					{
						if ( IsIPAddressV4Full( out ip1 ) )
						{
							switch ( NextToken() )
							{
								case Token.New:
									return Token.New;
								case Token.Range:
									if ( IsIPAddressV4Full( out ip2 ) )
									{
										token = NextToken();
										if ( token == Token.New || token == Token.End )
										{
											return Token.Range;
										}
										else
										{
											return Token.Unknown;
										}
									}
									return Token.Unknown;
								case Token.CIDR:
									if ( IsCIDR( out CIDR ) && IsCIDRRange( ip1, CIDR, out ip1, out ip2 ) )
									{
										token = NextToken();
										if ( token == Token.New || token == Token.End )
										{
											return Token.CIDR;
										}
										else
										{
											return Token.Unknown;
										}
									}
									return Token.Unknown;
								case Token.End:
									return Token.End;
								default:
									return Token.Unknown;
							}
						}
						else
						{
							return Token.Unknown;
						}
					}
					else if ( Current == '!' )
					{
						switch ( NextToken() )
						{
							case Token.Exclude:
								if ( IsIPAddressV4Full( out ip1 ) )
								{
									switch ( NextToken() )
									{
										case Token.New:
											return Token.ExcludeIP;
										case Token.Range:
											if ( IsIPAddressV4Full( out ip2 ) )
											{
												token = NextToken();
												if ( token == Token.New || token == Token.End )
												{
													return Token.ExcludeRange;
												}
												else
												{
													return Token.ExcludeUnknown;
												}
											}
											break;
										case Token.CIDR:
											if ( IsCIDR( out CIDR ) && IsCIDRRange( ip1, CIDR, out ip1, out ip2 ) )
											{
												token = NextToken();
												if ( token == Token.New || token == Token.End )
												{
													return Token.ExcludeCIDR;
												}
												else
												{
													return Token.Unknown;
												}
											}
											return Token.ExcludeCIDR;
										case Token.End:
											return Token.ExcludeEnd;
										default:
											return Token.ExcludeUnknown;
										}
									}
									else
									{
										return Token.End;
									}
								return Token.ExcludeUnknown;
							case Token.End:
								return Token.End;
							default:
								return Token.ExcludeUnknown;

						}
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
					if ( IsIPAddressV4Full( out ipTo ) )
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

			public bool IsCIDRRange( IPAddressV4 ip, int maskBits, out IPAddressV4 start, out IPAddressV4 end )
			{
				start = new IPAddressV4();
				end = new IPAddressV4();
				int mask = ~( ( 1 << ( 32 - maskBits ) ) - 1 );
				int StartIP = ip.Address & mask;
				int EndIP = ( ip.Address & mask ) | ~mask;
				List<uint> IPs = new List<uint>();

				//dodge an infinite loop
				if ( StartIP < 0 )
				{
					var temp = EndIP;
					EndIP = StartIP;
					StartIP = temp;
				}
				uint startIP = (uint)StartIP;
				uint endIP = (uint)EndIP;
				if ( startIP < endIP )
				{
					start.Address = (int)startIP;
					end.Address = (int)endIP;
					return true;
				}
				else if ( startIP > endIP )
				{
					end.Address = (int)startIP;
					start.Address = (int)endIP;
					return true;
				}
				return false;
			}

			private bool MatchChar( char c )
			{
				if ( IsNotEndOfInput && Current == ' ' )
				{
					Forward();
				}
				if ( IsNotEndOfInput && Current == c )
				{
					Forward();
					return true;
				}
				return false;
			}

			private bool IsByte( out byte b )
			{
				b = 0;
				string returnedByte = string.Empty;
				if ( Current == ' ' )
				{
					Forward();
				}
				while (IsNotEndOfInput && Char.IsDigit( Current ))
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

			private bool IsCIDR( out int CIDR )
			{
				CIDR = 0;
				string CIDRString = string.Empty;
				if ( Current == ' ' )
				{
					Forward();
				}
				while ( IsNotEndOfInput && Char.IsDigit( Current ) )
				{
					CIDRString += Current;
					Forward();
				}
				if ( CIDRString != string.Empty )
				{
					if ( int.TryParse( CIDRString, out CIDR ) && CIDR >= 0 && CIDR <= 32 )
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