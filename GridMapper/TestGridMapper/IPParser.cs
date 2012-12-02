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
			string StringToTest = "10.8.11.248";
			IPParserResult Result = new IPParserResult(); 
			Result = IPParser.TryParse(StringToTest);
			if ( Result.HasError )
			{
				Console.WriteLine( Result.ErrorMessage +" error");
			}
			foreach ( var ip in Result.Result )
			{
				Console.WriteLine(ip.From + " - " + ip.To);
			}
		}
	}
}
