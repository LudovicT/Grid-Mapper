using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper
{
	public struct PortComputer
	{
		public UInt16 Port;
		public bool Status;

		//questiona poser est ce qu'on le garde pour le confort ou vaut mieu virer et l'utilisateur se demerde ?'
		public PortComputer( int port, bool status )
		{
			//a mettre en anglais
			if( port < 0 || port > 65535 ) throw new ArgumentOutOfRangeException( "Le port doit être comprit entre 0 et 65535" );
			Port = (UInt16) port;
			Status = status;
		}
		public PortComputer( UInt16 port, bool status )
		{
			Port = port;
			Status = status;
		}

		public override string ToString()
		{
			return String.Format( "Port : {0}", Port );
		}

		public override bool Equals( object obj )
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}
}
