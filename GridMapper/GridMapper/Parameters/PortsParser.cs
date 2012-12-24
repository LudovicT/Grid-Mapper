using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper
{
	public class PortsParserResult
	{
		internal PortsParserResult( string errorMessage, IEnumerable<int> result )
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}
		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public IEnumerable<int> Result { get; private set; }
	}

	public static class PortsParser
	{
		public enum Token
		{
			Unknown,
			ExcludeUnknown,
			New,
			Range,
			End,
			Exclude,
			ExcludePort,
			ExcludeRange,
			ExcludeEnd,
		}

		public static PortsParserResult Tryparse( string PortsToParse )
		{
			string errorMessage = null;
			if ( PortsToParse == string.Empty )
			{
				errorMessage = "No ports provided";
				return new PortsParserResult( errorMessage, null );
			}
			ListOfRangeOfIntWithAutoCompression PortsList = new ListOfRangeOfIntWithAutoCompression();
			Parser parser = new Parser( PortsToParse );
			ushort port1;
			ushort port2;

			while ( parser.IsNotEndOfInput )
			{
				switch ( parser.NextToken( out port1, out port2 ) )
				{
					case Token.New:
					case Token.End:
						PortsList.Add( port1 );
						break;
					case Token.Range:
						PortsList.Add( port1, port2 );
						break;
					case Token.ExcludePort:
					case Token.ExcludeEnd:
						PortsList.Remove( port1 );
						break;
					case Token.ExcludeRange:
						PortsList.Remove( port1, port2 );
						break;
					case Token.Unknown:
					case Token.ExcludeUnknown:
						errorMessage = "Invalid format for IP Arguments";
						return new PortsParserResult( errorMessage, null );
				}
			}
			return new PortsParserResult( errorMessage, PortsList );
		}

		class Parser
		{
			readonly string _s;
			int _pos;

			public Parser( string s )
			{
				_s = s;
				_pos = 0;
			}

			char Current { get { return _s[_pos]; } }

			bool Forward()
			{
				return ++_pos < _s.Length;
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			public bool IsNotEndOfInput { get { return !IsEndOfInput; } }

			public Token NextToken()
			{
				if ( IsNotEndOfInput )
				{
					switch ( Current) 
					{
						case ',':
							Forward();
							return Token.New;
						case '-':
							Forward();
							return Token.Range;
						case '!':
							Forward();
							return Token.Exclude;
						default :
							return Token.Unknown;
					}
				}
				return Token.End;
			}

			public Token NextToken( out ushort port1, out ushort port2 )
			{
				port2 = 0;
				port1 = 0;
				Token token;
				if ( IsNotEndOfInput )
				{
					if ( Current != '!' )
					{
						if ( IsPort( out port1 ) )
						{
							switch ( NextToken() )
							{
								case Token.New:
									return Token.New;
								case Token.Range:
									if ( IsPort( out port2 ) )
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
								if ( IsPort( out port1 ) )
								{

									switch ( NextToken() )
									{
										case Token.New:
											return Token.ExcludePort;
										case Token.Range:
											if ( IsPort( out port2 ) )
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

			private bool MatchChar( char c )
			{
				if ( IsNotEndOfInput && Current == c )
				{
					Forward();
					return true;
				}
				return false;
			}

			public bool IsPort( out ushort port )
			{
				port = 0;
				string returnedport = String.Empty;
				while ( IsNotEndOfInput && Char.IsDigit( Current ) )
				{
					returnedport += Current;
					Forward();
				}
				if ( returnedport != String.Empty )
				{
					if ( ushort.TryParse( returnedport, out port ) )
					{
						if ( port == 0 )
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}

		}
	}
}
