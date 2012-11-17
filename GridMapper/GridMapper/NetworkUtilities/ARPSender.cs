using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading.Tasks;
using GridMapper.NetworkModelObject;
using System.Net.NetworkInformation;

namespace GridMapper
{
	public static  partial class NetworkUtilities
	{
		[DllImport( "iphlpapi.dll", ExactSpelling = true )]
		public static extern int SendARP( int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen );


		/// <summary>
		/// Get a set of MACs from a set of Ips using a set number of task
		/// </summary>
		/// <param name="Ips">The list of IpAddress</param>
		/// <param name="maxTask">The maximum number of tasks running in parallel</param>
		public static void GlobalTaskGetMacAddress( List<IPAddress> Ips, int maxTask )
		{
			List<Task> Tasks = new List<Task>();
			for( int i = 0 ; i < Ips.Count ; i++ )
			{
				int value = i;
				Tasks.Add( TaskGetMacAddress( Ips[value] ) );

				//wait for the maximum number of parallel tasks to be completed, be it successful or not
				if( i % maxTask == 0 )
				{
					Task.WaitAll( Tasks.ToArray() );
				}
			}
		}

		/// <summary>
		/// A task getting the MAC from an Ip
		/// </summary>
		/// <param name="Ip">The ip address to get in MAC form</param>
		/// <returns>The created task</returns>
		public static Task TaskGetMacAddress( IPAddress Ip )
		{
			//start the task using aa lamba function and return the task for future uses
			Task task = Task.Factory.StartNew( () =>
			{
				Network.MacAddressHandling( Ip, GetMacAddress( Ip ) );
			} );
			return task;
		}

		/// <summary>
		/// Get the MAC addresse from any given Ip which is reachable
		/// </summary>
		/// <param name="dst">The ip which we would like to get the MAC address</param>
		/// <returns>The MAC address</returns>
		public static PhysicalAddress GetMacAddress( IPAddress dst )
		{
			int intAddress = BitConverter.ToInt32( dst.GetAddressBytes(), 0 );
			byte[] macAddr = new byte[6];
			uint macAddrLen = (uint)macAddr.Length;
			if( SendARP( intAddress, 0, macAddr, ref macAddrLen ) != 0 )
			{
				throw new InvalidOperationException( "SendARP failed." );
			}

			string[] str = new string[(int)macAddrLen];
			for( int i = 0 ; i < macAddrLen ; i++ )
				str[i] = macAddr[i].ToString( "x2" );
			string fullMac = string.Join( "", str ).ToUpper();
			return PhysicalAddress.Parse( fullMac );
		}
	}
}
