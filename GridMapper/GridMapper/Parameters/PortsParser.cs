using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GridMapper.Parameters
{
	public class PortsParserResult
	{
		internal PortsParserResult ( string errorMessage, IEnumerable<Int32> result )
		{
			Debug.Assert( ( errorMessage == null ) == ( result != null ) );
			ErrorMessage = errorMessage;
			Result = result;
		}
		public bool HasError { get { return ErrorMessage != null; } }
		public string ErrorMessage { get; private set; }
		public IEnumerable<Int32> Result { get; private set; }
	}
	public static class PortsParser
	{
		public static PortsParserResult PortsParser( string PortsToParse )
		{
			List<Int32> PortsList = new List<Int32>();
			string errorMessage = String.Empty;

			
			return new PortsParserResult( errorMessage, PortsList );
		}
	}
}
