﻿using System;
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
		public void StandardInputNoRangeNoExclude()
		{
			TryPortsParser( "32,33,34,76,999,65365" );
		}
		[Test]
		public void StandardInputNoExclude()
		{
			TryPortsParser( "32,33,34,76,80-443" );
		}
		[Test]
		public void StandardInputNoRange()
		{
			TryPortsParser( "32,33,34,76,!33,!34" );
		}
		[Test]
		public void StandardInput()
		{
			TryPortsParser( "32-38,80,443,!33-36,!37" );
		}
		[Test]
		public void MoreThanOneComma()
		{
			TryPortsParser( "32,33,,,,55" );
		}
		[Test]
		public void SpacesBetweenValues()
		{
			TryPortsParser( "43  ,  21" );
		}
		[Test]
		public void InvalidValues()
		{
			TryPortsParser( "Hello, am I a port to be scanned ? That is the question !" );
		}
		[Test]
		public void InexistentPorts()
		{
			TryPortsParser( "78568" );
			TryPortsParser( "0" );
		}
		[Test]
		public void TestForNegativeValues()
		{
			TryPortsParser( "-1" );
			TryPortsParser( "-340" );
		}
		//[Test]  --> trigger Debug.Assert ... so it is not wise !
		//public void NoPortsProvided()
		//{
		//    TryPortsParser( "" );
		//}
		internal void TryPortsParser( string PortsToParse )
		{
			Console.WriteLine( "Parsing : \'" + PortsToParse + "\' :" );
			PortsParserResult PortsToHandle = PortsParser.Tryparse( PortsToParse );
			if (PortsToHandle.HasError == true)
			{
				Console.WriteLine( PortsToHandle.ErrorMessage );
			}
			else
			{
				foreach ( ushort port in PortsToHandle.Result )
				{
					Console.WriteLine( port.ToString() );
				}
			}
		}
	}
}
