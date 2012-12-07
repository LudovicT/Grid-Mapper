using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper
{
	public class PortsParserResult
	{
		internal PortsParserResult(string errorMessage, List<ushort> result)
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}
		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public List<ushort> Result { get; private set; }
	}
	public static class PortsParser
	{
		public static PortsParserResult MainPortsParser( string PortsToParse )
		{
			List<ushort> PortsList = new List<ushort>();
			string errorMessage = null;
			Parser parser = new Parser( PortsToParse );
			ushort currentPort;

			if (PortsToParse == string.Empty)
				errorMessage = "No ports provided";
			else
			{
				while (parser.NextToken() != Token.End)
				{
					switch ( parser.NextToken( out currentPort) )
					{
						case Token.New:
						case Token.End:
							PortsList.Add(currentPort);
							break;
						case Token.Unknown:
							errorMessage = "Invalid format for ports arguments";
							return new PortsParserResult(errorMessage, null);
					}
				}
			}
			return new PortsParserResult( errorMessage, PortsList);
		}
		public enum Token
		{
			Unknown,
			New,
			End,
		}

		class Parser
		{
			readonly string _s;
			int _pos;

			public Parser(string s)
			{
				_s = s;
				_pos = 0;
			}

			public Token NextToken()
			{
				if ( !IsEndOfInput )
				{
					switch ( Current) 
					{
						case ',' :
							Next();
							return Token.New;
						default :
							return Token.Unknown;
					}
				}
				return Token.End;
			}

			public Token NextToken(out ushort us1)
			{
				us1 = 0;
				if (!IsEndOfInput)
				{
					if (IsPort( out us1 ))
					{
					}
				}
				return Token.End;
			}

			char Current { get { return _s[ _pos ]; } }

			bool Next()
			{
				// Skip whitespaces...
				if (Current == ' ')
				{
					while ( !IsEndOfInput && Current == ' ' ) _pos++;
					return true;
				}
				else
					return ++_pos < _s.Length;
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			private bool MatchChar( char c )
			{
				if ( !IsEndOfInput && Current == ' ' )
				{
					Next();
				}
				if ( !IsEndOfInput && Current == c )
				{
					Next();
					return true;
				}
				return false;
			}

			public bool IsPort(out ushort us1)
			{
				us1 = 0;
				return IsUShort( out us1 );
			}

			public bool IsUShort(out ushort us1)
			{
				us1 = 0;
				string returnedUShort = String.Empty;
				if (Current == ' ')
				{
					Next();
				}
				while (!IsEndOfInput && Char.IsDigit(Current))
				{
					returnedUShort += Current;
					Next();
				}

				if (returnedUShort != String.Empty)
				{
					if (ushort.TryParse(returnedUShort, out us1))
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
