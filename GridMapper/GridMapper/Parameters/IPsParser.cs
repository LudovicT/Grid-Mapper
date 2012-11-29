using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.Parameters
{
	class IPsParser
	{
		public List<IPAddress> IPArgumentsParser(string IPToParse)
		{
			List<IPAddress> ListIPs = new List<IPAddress>();
			Console.WriteLine( "IPsToParse = \'" + IPToParse + "\'" );
			string[] BigBlocksIP = IPToParse.Split( ',' );
			if ( BigBlocksIP.Length > 1 )
			{
				for ( int iterator = 0; iterator < BigBlocksIP.Length; iterator++ )
				{
					string[] SmallBlocksIP = BigBlocksIP[ iterator ].Split( '-' );
					if ( SmallBlocksIP.Length > 1 )
					{
						for ( int iterator2 = 0; iterator2 < SmallBlocksIP.Length; iterator2++ )
						{
							if ( ( iterator2 + 1 ) < SmallBlocksIP.Length )
							{
								IPAddress StartingIP = IPAddress.Parse( SmallBlocksIP[ iterator2 ] );
								IPAddress EndingIP = IPAddress.Parse( SmallBlocksIP[ iterator2 + 1 ] );
								Console.WriteLine("SIP " + StartingIP);
								Console.WriteLine("EIP " + EndingIP);
								ListIPs = IPRangeGenerator( ListIPs, StartingIP, EndingIP );
								continue;
							}
						}
					}
					else
					{
						ListIPs.Add( IPAddress.Parse( BigBlocksIP[ iterator ] ) ) ;
					}
				}
			}
			else
			{
				ListIPs.Add( IPAddress.Parse( IPToParse ) );
			}
			return ListIPs;
		}
		private List<IPAddress> IPRangeGenerator( List<IPAddress> ListIPs, IPAddress StartingIP, IPAddress EndingIP )
		{
			for ( uint IPToGenerate = IPRange.IPAddressToUint( StartingIP ); IPToGenerate <= IPRange.IPAddressToUint( EndingIP ); IPToGenerate++ )
			{
				ListIPs.Add( IPAddress.Parse( IPToGenerate.ToString() ));
			}
			return ListIPs;
		}
	}
}
