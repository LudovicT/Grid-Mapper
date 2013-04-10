using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace GridMapper.NetworkModelObject
{
	public interface IHostEntity
	{
		IServer Server { get; }
		IFirewall Firewall { get; }
		IOS OS { get; }
		IPorts Ports { get; }
		ReadOnlyCollection<IHost> Hosts { get; }
		string NameOS { get; }
		//vérifier si sa position ici est justifié
		string HostName { get; }

		void Describe( TextWriter output );
	}
}
