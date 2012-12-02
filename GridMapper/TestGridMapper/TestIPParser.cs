using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GridMapper;

namespace GridMapper.Test
{
	[TestFixture]
	public class IPParserTest
	{
		[Test]
		public void OneIPAddress()
		{
			tryresult( "0.0.0.0" );
		}
		[Test]
		public void TwoIPAddress()
		{
			tryresult( "0.0.0.0,1.1.1.1" );
		}
		[Test]
		public void OneIPAddressRange()
		{
			tryresult( "0.0.0.0-1.1.1.1" );
		}
		[Test]
		public void TwoIPAddressRange()
		{
			tryresult( "0.0.0.0-1.1.1.1,2.2.2.2-3.3.3.3" );
		}
		[Test]
		public void OneIPAddressShort()
		{
			tryresult( "0.1" );
		}
		[Test]
		public void OneIPAddressRangeShort()
		{
			tryresult( "0.0-1.1" );
		}
		[Test]
		public void OneIPAddressAndIPAddressRange()
		{
			tryresult( "0.0.0.0,1.1.1.1-2.2.2.2" );
		}
		[Test]
		public void OneIPAddressRangeAndIPAddress()
		{
			tryresult( "0.0.0.0-1.1.1.1,2.2.2.2" );
		}
		[Test]
		public void MalformatedIPAddressRange()
		{
			tryresult( "0.0.0.0--1.1.1.1" );
		}
		[Test]
		public void WhitespaceIPAddress()
		{
			tryresult( "   0 . 0 . 0 . 0" );
		}

		internal void tryresult (string StringToTest)
		{
			Console.WriteLine( "Testing string : " + StringToTest );
			IPParserResult Result = new IPParserResult();
			Result = IPParser.TryParse( StringToTest );
			if ( Result.HasError )
			{
				Console.WriteLine( Result.ErrorMessage + " error" );
			}
			foreach ( var ip in Result.Result )
			{
				Console.WriteLine( ip.From + " - " + ip.To );
			}
		}
	}
}
