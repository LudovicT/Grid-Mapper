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
		public bool Ping { get; set; }
		public bool Arping { get; set; }
		public bool Arp { get; set; }
		public bool Dns { get; set; }
		public bool Port { get; set; }
		public bool RandomTCPPort { get; set; }
		public bool RandomUDPPort { get; set; }
		public bool CmdConsole { get; set; }
		public int MaximumTasks { get; set; }
		public int PingTimeout { get; set; }
		public int NbPacketToSend { get; set; }
		public int WaitTime { get; set; }
		public ushort TCPPort { get; set; }
		public ushort UDPPort { get; set; }
		public IPParserResult IpToTest { get; set; }
		string _portToTestString;
		PortsParserResult _portToTest;

		// This constructor will set the required options to their default values in order for the program to work properly
		public Option()
		{
			PortToTestString = "22,80,443";
			Ping = true;
			Arping = true;
			Arp = true;
			Dns = true;
			Port = true;
			RandomTCPPort = false;
			RandomUDPPort = false;
			CmdConsole = false;
			MaximumTasks = 50;
			PingTimeout = 1000;
			NbPacketToSend = 10;
			WaitTime = 1;
			TCPPort = 62000;
			UDPPort = 62001;
			IpToTest = null;
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
		public int PortToTestCount
		{
			get
			{
				int i = 0;
				if ( PortToTest.Result != null )
				{
					foreach ( int ip in PortToTest.Result )
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
				if ( Port )
				{
					i++;
					i = i + PortToTestCount;
				}
                return i;
            }
		}
		public string PortToTestString
		{
			get
			{
				return _portToTestString;
			}
			set
			{
				_portToTestString = value;
				_portToTest = PortsParser.Tryparse( value );
			}
		}
		public PortsParserResult PortToTest
		{
			get
			{
				return _portToTest;
			}
			set
			{
				_portToTest = value;
			}
		}
		public int TotalOperation
		{
			get
			{
				return IPToTestCount * OperationCount;
			}
		}

	}
	#endregion // Option
}
