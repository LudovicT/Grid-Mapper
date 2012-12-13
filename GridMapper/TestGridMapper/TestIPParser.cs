using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GridMapper;
using System.Net;

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
			tryresult( "0.0.0.0,0.0.1.1" );
		}
		[Test]
		public void OneIPAddressRange()
		{
			tryresult( "0.0.0.0-0.0.1.1" );
		}
		[Test]
		public void TwoIPAddressRange()
		{
			tryresult( "0.0.0.0-0.0.1.1,2.2.2.2-2.2.3.3" );
		}
		[Test]
		public void OneIPAddressShort()
		{
			tryresult( "0.1", string.Empty );
		}
		[Test]
		public void OneIPAddressRangeShort()
		{
			tryresult( "0.0-0.0.1", string.Empty );
		}
		[Test]
		public void OneIPAddressAndIPAddressRange()
		{
			tryresult( "0.0.0.0,1.1.1.1-1.1.2.2" );
		}
		[Test]
		public void OneIPAddressRangeAndIPAddress()
		{
			tryresult( "0.0.0.0-0.0.1.1,2.2.2.2" );
		}
		[Test]
		public void BigRange()
		{
			tryresult( "0-255" , string.Empty);
		}
		[Test]
		public void MalformatedIPAddressRange()
		{
			tryresult( "0.0.0.0--0.0.1.1", string.Empty );
		}
		[Test]
		public void MalformatedIPAddressRange2()
		{
			tryresult( "0.0.0.0,,0.0.1.1", string.Empty );
		}
		[Test]
		public void WhitespaceIPAddress()
		{
			tryresult( "   0 . 0 . 0 . 0", string.Empty );
		}
		[Test]
		public void RemoveOneIPAddress()
		{
			tryresult( "0.0.0.0,1.1.1.1,!1.1.1.1" );
		}
		[Test]
		public void RemoveTwoIPAddress()
		{
			tryresult( "0.0.0.0,0.0.0.1,1.1.1.1,!0.0.0.0,!1.1.1.1" );
		}
		[Test]
		public void RemoveOneIPRange()
		{
			tryresult( "0.0.0.0,0.1.1.1,1.1.1.1,!0.0.1.1-2.2.2.2" );
		}
		[Test]
		public void RemoveFullRangeTest()
		{
			tryresult( "0.0.0.0-0.0.5.255,!0.0.1.1-0.0.3.2,!0.0.3.255-0.0.4.200" );
		}
		[Test]
		public void AddTwiceSameIPAddress()
		{
			tryresult( "0.0.0.0,0.0.0.0" );
		}
		[Test]
		public void AddTwiceSameIPRangeAddress()
		{
			tryresult( "0.0.0.0-0.0.1.1,0.0.0.0-0.0.1.1" );
		}

		[Test]
		public void IPAddress192()
		{
			tryresult( "192.168.1.255" );
		}

		internal void tryresult (string StringToTest, string assert = null)
		{
			Console.WriteLine( "Testing string : " + StringToTest );
			IPParserResult Result = new IPParserResult();
			Result = IPParser.TryParse( StringToTest );
			if ( Result.HasError )
			{
				Console.WriteLine( Result.ErrorMessage + " error" );
				if ( assert != null )
				{
					Console.WriteLine( "WINNING LOT" );
				}
			}
			else
			{
				foreach ( var ip in Result.Result )
				{
					Console.WriteLine(IPAddress.Parse(((uint)ip).ToString()).ToString());
				}
			}
		}
	}
}
