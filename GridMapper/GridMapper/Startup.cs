﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper
{

	class Startup
	{
		bool _cmdConsole;
		int _maximumTasks;
		List<IPAddress> _ipToTest;
		int _pingTimeout;

		public Startup()
		{
			_cmdConsole = false;
			_maximumTasks = 50;
			//_ipToTest = getLocalIPRange();
			int _pingTimeout = 1000;
		}

		public bool CmdConsole
		{
			get
			{
				return _cmdConsole;
			}
			set
			{
				_cmdConsole = value;
			}
		}

		public int MaximumTasks
		{
			get
			{
				return _maximumTasks;
			}
			set
			{
				_maximumTasks = value;
			}
		}

		public int PingTimeout
		{
			get
			{
				return _pingTimeout;
			}
			set
			{
				_pingTimeout = value;
			}
		}

		public List<IPAddress> IpToTest
		{
			get
			{
				return _ipToTest;
			}
			set
			{
				_ipToTest = value;
			}
		}
	}
}
