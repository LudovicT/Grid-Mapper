using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.NetworkUtilities
{
	/// <summary>
	/// This is the main class for all network-related features.
	/// These features are as follow :
	/// ARP (Address Resolution Protocol) sender to retrieve the MAC address of a machine
	/// Ping sender to send a request to a machine
	/// Reverse DNS (Domain Name System) to match an IP Address to a machine
	/// WMI Collector retrieves information from a machine such as the operating system the machine runs
	/// </summary>
	public class NetworkSender
	{
		ARPSender _arpSender;
		PingSender _pingSender;
		ReverseDnsResolver _reverseDnsResolver;
		PortScanner _portScanner;
		WMICollector _wmiCollector;

		Option _option;

		public NetworkSender(Option option)
		{
			_arpSender = new ARPSender();
			_pingSender = new PingSender( option );
			_reverseDnsResolver = new ReverseDnsResolver();
			_portScanner = new PortScanner();
			_wmiCollector = new WMICollector();

			_option = option;
		}
	}
}
