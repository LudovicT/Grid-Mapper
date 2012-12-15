using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GridMapper
{

	public class Option
	{
		public readonly bool Ping = true;
		public bool Arp = true;
		public bool Dns = true;
		public bool Port = true;
		bool _cmdConsole;
		int _maximumTasks;
		IPParserResult _ipToTest;
		int _pingTimeout;

		public Option()
		{
			_cmdConsole = false;
			_maximumTasks = 50;
			_ipToTest = null;
			_pingTimeout = 1000;
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

		public IPParserResult IpToTest
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

		public int IPToTestCount
		{
			get
			{
				int i = 0;
				if ( _ipToTest.Result != null )
				{
					foreach ( int ip in _ipToTest.Result )
					{
						i++;
					}
					return i;
				}
				return 0;
			}
		}
        public int OperationCount
        {
            get
            {
                int i = 0;
                if ( Ping ) i++;
                if ( Arp ) i++;
				if ( Dns ) i++;
				if ( Port ) i++;
                return i;
            }
        }

        
	}
}
