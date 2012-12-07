using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper
{
	public class PortsParserResult
	{
		internal PortsParserResult(string errorMessage, List<Int32> result)
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}
		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public List<Int32> Result { get; private set; }
	}
	public static class PortsParser
	{
		public static PortsParserResult MainPortsParser( string PortsToParse )
		{
			List<Int32> PortsList = new List<Int32>();
			string errorMessage = String.Empty;

			Parser parser = new Parser( PortsToParse );
			if ( !parser.ParseNow() )
				errorMessage = "Something went wrong while parsing ports arguments";

			return new PortsParserResult( errorMessage, parser.PortsList);
		}

		class Parser
		{
			readonly string[] _s;
			public List<Int32> PortsList;
			int _pos;

			public Parser(string s)
			{
				string[]  _s = s.Split( ',' );
				PortsList = new List<Int32>();
				_pos = -1;
			}
			string Current { get { return _s[ _pos ]; } }

			bool Next()
			{
				if ( !IsEndOfInput )
					return ++_pos < _s.Length;
				return false;
			}

			bool IsEndOfInput { get { return _pos >= _s.Length; } }

			public bool ParseNow()
			{
				Int32 port;
				if ( Next() )
				{
					if ( Int32.TryParse( Current, out port ) )
					{
						if ( port < 0 || port > 65535 )
							return false;
						else
						{
							PortsList.Add( port );
							return true;
						}
					}
				}
				return false;
			}
		}
	}
}
