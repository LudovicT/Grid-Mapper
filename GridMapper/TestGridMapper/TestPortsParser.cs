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
			TryPortsParser("32, 33");
		}
		internal void TryPortsParser( string PortsToParse )
		{
			PortsParserResult PortsToHandle = PortsParser.MainPortsParser( PortsToParse );
			if (PortsToHandle.HasError == true)
			{
				Console.WriteLine(PortsToHandle.ErrorMessage);
			}
			else
			{
				foreach (ushort port in PortsToHandle.Result)
				{
					Console.WriteLine(port);
				}
			}
		}
	}
}
