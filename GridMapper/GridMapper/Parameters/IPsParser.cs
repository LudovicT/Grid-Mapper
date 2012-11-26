using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.Parameters
{
	class IPsParser
	{
		public List<IPAddress> /*string[]*/ IPArgumentsParser(string IPToParse)
		{
			List<IPAddress> ListIPs = new List<IPAddress>();
			string[] BigBlocksIP = IPToParse.Split( ',' );
			for (int iterator = 0; iterator < BigBlocksIP.Length; iterator++)
			{
				string[] SmallBlocksIP = BigBlocksIP[ iterator ].Split( '-' );
				if (SmallBlocksIP.Length > 1)
				{
					for (int iterator2 = 0; iterator2 < SmallBlocksIP.Length; iterator2++)
					{
						if ( (iterator2 + 1) < SmallBlocksIP.Length )
						{
							IPAddress StartingIP = IPAddress.Parse(SmallBlocksIP[iterator2]);
							IPAddress EndingIP = IPAddress.Parse(SmallBlocksIP[iterator2 + 1]);
							Console.WriteLine(StartingIP);
							Console.WriteLine(EndingIP);
						}
						
						ListIPs.Add(IPAddress.Parse(SmallBlocksIP[iterator2]));
					}
				}
			}
			return ListIPs;
		}
	}
}
