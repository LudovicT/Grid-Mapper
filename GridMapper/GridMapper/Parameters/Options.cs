using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper
{
	#region Option

	/// <summary>
	/// Option class
	/// </summary>
	/// 
	public class Option
	{
		/// <summary>
		/// This class handle all the options provided in the GUI.
		/// It also makes sure that the options that are set are applied in the whole process.
		/// <param name="Ping"> A boolean to check if the ping feature is to be performed or not.</param>
		/// <param name="Arping">A boolean to check if the ARP (Address Resolution Protocol) ping feature is to be performed or not.</param>
		/// <param name="Arp"> A boolean to check if the ARP feature is to be performed or not.</param>
		/// <param name="Dns"> A boolean to check if the DNS of a machine is to be retrieved or not.</param>
		/// <param name="_cmdConsole"> A boolean to check if the Console Mode will be used instead of the GUI.</param>
		/// <param name="_maximumTasks">This interger is to limit the number of simultaneous (asynchroneous) tasks. Too many taks may render the computer unstable and in worse case, it will freeze</param>
		/// <param name="_ipToTest">This is a IEnumerable where all the IPs to be tested are stored.</param>
		/// <param name="_pingTimeout">This value is to set a time out for all the pings that will be performed. Once reached, the programs abandon the current pings.</param>
		/// <param name="_portToTest">This is a IEnumerable where all the ports to be scanned are stored.</param>
		/// </summary>
		/// 
		public bool Ping = true;
		public bool Arping = true;
		public bool Arp = true;
		public bool Dns = true;
		public bool Port = true;
		public bool RandomTCPPort = false;
		public bool RandomUDPPort = false;
		public bool CmdConsole = false;
		public int MaximumTasks = 50;
		public int PingTimeout = 1000;
		public int NbPacketToSend = 10;
		public int WaitTime = 1;
		public ushort TCPPort = 62000;
		public ushort UDPPort = 62001;
		public IPParserResult IpToTest;
		public string PortToTest = string.Empty;

		// This constructor will set the required options to their default values in order for the program to work properly
		public Option()
		{
			IpToTest = null;
			PortToTest = "22,80,443";
		}


		// This getter retunrs the number of IP to be tested
		public int IPToTestCount
		{
			get
			{
				int i = 0;
				if ( IpToTest.Result != null )
				{
					foreach ( int ip in IpToTest.Result )
					{
						i++;
					}
					return i;
				}
				return 0;
			}
		}

		// This getter retunrs the number of options that are set.
        public int OperationCount
        {
            get
            {
                int i = 0;
				if ( Ping ) i++;
				if ( Arping ) i++;
				if ( Arp ) i++;
				if ( Dns ) i++;
				if ( Port ) i++;
                return i;
            }
        }

	}
	#endregion // Option
}
