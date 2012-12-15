using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace GridMapper.NetworkModelObject
{
	public class HostEntity : IHostEntity
	{
		ReadOnlyCollection<IHost> _hosts;
		string _nameOS;
		string _hostName;
		IPorts _ports;
		IServer _server;
		IFirewall _firewall;
		IOS _OS;

		#region IHostEntity Membres

		public ReadOnlyCollection<IHost> Hosts
		{
			get { return _hosts; }
		}

		public string NameOS
		{
			get { return _nameOS; }
		}

		public string HostName
		{
			get { return _hostName; }
		}

		public IPorts Ports
		{
			get { return _ports; }
		}

		public IServer Server
		{
			get { return _server; }
		}

		public IFirewall Firewall
		{
			get { return _firewall; }
		}

		public IOS OS
		{
			get { return _OS; }
		}

		#endregion

		public HostEntity( IList<IHost> hosts, string hostName)
			: this( hosts, string.Empty, hostName, null, null, null, null )
		{
		}

		public HostEntity( IList<IHost> hosts, string nameOs, string hostName, IPorts ports )
			: this( hosts, nameOs, hostName, ports, null, null, null )
		{
		}

		public HostEntity(IList<IHost> hosts, string nameOs, string hostName, IPorts ports, IServer server, IFirewall firewall, IOS OS)
		{
			_hosts = new ReadOnlyCollection<IHost>(hosts);
			_nameOS = nameOs;
			_hostName = hostName;
			_ports = ports;
			_server = server;
			_firewall = firewall;
			_OS = OS;
		}

		public void Describe( TextWriter output )
		{
			for(int i=0;i<_hosts.Count;i++)
			{
				output.WriteLine( "Host n°{0} : ", i );
				_hosts[i].Describe(output);
			}
			output.WriteLine( "Host name : {0}", _hostName );
			output.WriteLine( "OS name : {0}", _nameOS );
			//_ports.Describe( output );
			//_firewall.Describe( output );
			//_server.Describe( output );
			//_OS.describe( output );
		}
	}
}
