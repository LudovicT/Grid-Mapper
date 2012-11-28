using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper.NetworkUtilities
{
	public class NetworkSender
	{
		ARPSender _arpSender;
		PingSender _pingSender;
		ReverseDnsResolver _reverseDnsResolver;
		PortScanner _portScanner;
		WMICollector _wmiCollector;

		Option _option;
		TaskManager _taskManager;

		public NetworkSender(Option option)
		{
			_arpSender = new ARPSender();
			_pingSender = new PingSender( option );
			_reverseDnsResolver = new ReverseDnsResolver();
			_portScanner = new PortScanner();
			_wmiCollector = new WMICollector();

			_option = option;
			_taskManager = new TaskManager();
		}
	}
}
