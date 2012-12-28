using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper
{
	#region PortsParser

	/// <summary>
	/// PortsParserResult class
	/// </summary>
	/// 
	public class PortsParserResult
	{
		/// <summary>
		/// This class will call the parser to parse the ports and will return the list of ports to be scanned 
		/// or an error with a message if it did not work.
		/// <param name="PortsToParse">The list of ports to be parsed.</param>
		/// <param name="errorMessage"> This message is set and returned when something went wrong while parsing the ports</param>
		/// <param name="result"> This is where the ports to be scanned are stored.</param>
		/// <returns>
		/// <c>IEnumerable<int>Result</c>
		/// </returns>
		/// </summary>
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
	/// <summary>
	/// This class is the parser for the ports arguments.
	/// This parser will retrieve all the ports to be scanned.
	/// It includes features such as 
	/// simple parsing (single ports), 
	/// range (from -> to) 
	/// and exclusion (single and range)
	/// </summary>
	public static class PortsParser
	{
		// These tokens are to identify specific type of ports.
		public enum Token
		{
			Unknown,		//when an invalid port is being parsed
			ExcludeUnknown,
			New,			// when a new port is to be parsed
			Range,			// when a range of ports is to be generated
			End,			// when the parsed reached end of the ports to be parsed
			Exclude,		// when an exclusion is to be applied
			ExcludePort,	// when a single port is to be excluded from the final list
			ExcludeRange,	// when a range of ports is to be excluded from the final list. NB: the range will be generated as well
			ExcludeEnd,		// when the parsed reached end of the ports to be parsed but in a case of exclusion
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
						errorMessage = "Invalid format for ports Arguments";
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

			// this method is used to read the string character by character
			char Current { get { return _s[_pos]; } }

			// this method is used to move toward the next character
			bool Forward()
			{
				return ++_pos < _s.Length;
			}

			// this method is used to check if the parser has reached end of input
			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			// this method is used to check if the parser has not reached end of input
			public bool IsNotEndOfInput { get { return !IsEndOfInput; } }

			// this method is used to find out which token is to be applied considering the character the parser is analyzing.
			public Token NextToken()
			{
				if ( IsNotEndOfInput )
				{
					switch ( Current) 
					{
						case ',':
							Forward();
							return Token.New; // Here, a new port is to be parsed
						case '-':
							Forward();
							return Token.Range; // Here, a range of port is to be generated
						case '!':
							Forward();
							return Token.Exclude; // Here, an exclusion is to be applied
						default :
							return Token.Unknown; // When none of the above is possible. In this case, the parser will stop and return an error
					}
				}
				return Token.End;
			}
			// this method is basically used to find what token is to be returned when considering two newly parsed ports .
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

			// this method is used to check if character in parameter is the one the parser is currently reading.
			// if it matches, the parser moves toward the next character in the input
			private bool MatchChar( char c )
			{
				if ( IsNotEndOfInput && Current == c )
				{
					Forward();
					return true;
				}
				return false;
			}

			// this method is used to check if the parser retrieve a value that is a valid port.
			// if the value is a valid port, the port is returned to be added in the list
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
	#endregion //PortsParser
}
