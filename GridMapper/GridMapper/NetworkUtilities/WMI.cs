using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Threading.Tasks;

namespace GridMapper
{
	static public class WMICollector
	{
		static public Task WMI( IList<IPAddress> ipCollection )
		{
			Task task = Task.Factory.StartNew( () =>
			{
				foreach ( IPAddress ipAddress in ipCollection )
					WMI( ipAddress );
			} );
			return task;
		}

		/// <summary>
		/// Set the WMI Query tu be able to get the main information of the targeted computer
		/// </summary>
		/// <param name="Ip">The target of the WMI Query</param>
		/// <returns>The started task</returns>
		public static Task WMI( IPAddress Ip )
		{
			Dictionary<string, string> results = new Dictionary<string, string>();
			Task task = Task.Factory.StartNew( () =>
				{
					ManagementScope scope = new ManagementScope( @"\\" + Ip + @"\root\cimv2" );
					scope.Connect();
					WMIProcess( scope, "Name", "Win32_OperatingSystem", "Operation System Name" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "Caption", "Win32_OperatingSystem", "Windows Name" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "Name", "Win32_Processor", "CPU Make,Model" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "TotalPhysicalMemory", "Win32_ComputerSystem", "RAM Size" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "Model", "Win32_ComputerSystem", "Computer Model" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "SerialNumber", "Win32_SystemEnclosure", "Serial Number" ).ToList().ForEach( x => results[x.Key] = x.Value );
					WMIProcess( scope, "SMBIOSAssetTag", "Win32_SystemEnclosure", "Asset Tag" ).ToList().ForEach( x => results[x.Key] = x.Value );
				}
			);
			return task;
		}

		/// <summary>
		/// Query to WMI to get infos
		/// </summary>
		/// <param name="scope">The ManagementScope of the search</param>
		/// <param name="QuerySearch">The term searched</param>
		/// <param name="QueryLocation">The location of the search</param>
		/// <param name="Name">The name given to that search</param>
		/// <returns>A dictionary with the results</returns>
		static Dictionary<string,string> WMIProcess( ManagementScope scope, string QuerySearch, string QueryLocation , string Name = null)
		{
			Dictionary<string, string> results = new Dictionary<string, string>();
			SelectQuery query = new SelectQuery( "SELECT " + QuerySearch + " FROM " + QueryLocation );
			ManagementObjectSearcher searcher = new ManagementObjectSearcher( scope, query );
			ManagementObjectCollection queryCollection = searcher.Get();
			foreach ( ManagementObject foundObject in queryCollection )
			{
				if ( Name == null )
					results.Add( QuerySearch, foundObject[QuerySearch].ToString() );
				else
					results.Add( Name, foundObject[QuerySearch].ToString() );
			}

			if(results.Count == 1)
				return results;
			else
				return null;
		}
	}
}
