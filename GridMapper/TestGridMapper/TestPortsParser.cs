using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GridMapper;

namespace GridMapper.Test
{
	[TestFixture]
	public class TestPortsParser
	{
		[Test]
		public void PerformTestPortsParser()
		{
			TryPortsParser("42,56,887,987");
		}
		internal void TryPortsParser( string PortsToParse )
		{
			PortsParserResult PortsToHandle = PortsParser.MainPortsParser( PortsToParse );
			foreach ( Int32 port in PortsToHandle.Result )
			{
				Console.WriteLine( port );
			}
		}
	}
}
