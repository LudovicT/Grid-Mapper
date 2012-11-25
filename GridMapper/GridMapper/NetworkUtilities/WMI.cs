using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Threading.Tasks;

namespace GridMapper
{
	public class WMICollector
	{
		/// <summary>
		/// Get all the intersting info of WMI from an ip
		/// </summary>
		/// <param name="ipAddress">The target ip</param>
		/// <returns>A dictionary with the content searched</returns>
		public Dictionary<string,string> WMI( IPAddress ipAddress )
		{
			Dictionary<string, string> results = new Dictionary<string, string>();
			ManagementScope scope = new ManagementScope( @"\\" + ipAddress + @"\root\cimv2" );
			scope.Connect();
			WMIProcessQuery( scope, "Name", "Win32_OperatingSystem", "Operation System Name" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "Caption", "Win32_OperatingSystem", "Windows Name" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "Name", "Win32_Processor", "CPU Make,Model" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "TotalPhysicalMemory", "Win32_ComputerSystem", "RAM Size" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "Model", "Win32_ComputerSystem", "Computer Model" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "SerialNumber", "Win32_SystemEnclosure", "Serial Number" ).ToList().ForEach( x => results[x.Key] = x.Value );
			WMIProcessQuery( scope, "SMBIOSAssetTag", "Win32_SystemEnclosure", "Asset Tag" ).ToList().ForEach( x => results[x.Key] = x.Value );
			return results;
		}

		/// <summary>
		/// Query to WMI to get infos
		/// </summary>
		/// <param name="scope">The ManagementScope of the search</param>
		/// <param name="QuerySearch">The term searched</param>
		/// <param name="QueryLocation">The location of the search</param>
		/// <param name="Name">The name given to that search</param>
		/// <returns>A dictionary with the results</returns>
		Dictionary<string,string> WMIProcessQuery( ManagementScope scope, string QuerySearch, string QueryLocation , string Name = null)
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
