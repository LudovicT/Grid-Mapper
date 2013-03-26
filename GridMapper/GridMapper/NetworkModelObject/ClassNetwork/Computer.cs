using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject.ClassNetwork
{
	public class Computer : IComputer
	{
		public IServer Server
		{
			get { throw new NotImplementedException(); }
		}

		public IFirewall Firewall
		{
			get { throw new NotImplementedException(); }
		}

		public IOS OS
		{
			get { throw new NotImplementedException(); }
		}

		public IPorts Ports
		{
			get { throw new NotImplementedException(); }
		}

		//ToDo : need move in IOS
		public string NameOS
		{
			get { throw new NotImplementedException(); }
		}

		//ToDo : c'est pas a mettre la, a etudier
		public string HostName
		{
			get { throw new NotImplementedException(); }
		}

		public ReadOnlyCollection<IHost> Hosts
		{
			get { throw new NotImplementedException(); }
		}
	}
}
