﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject
{
	class Host : NetworkEntity, IHost
	{
		public Host()
		{
		}

		public Host( INetworkInterface networkInteface )
			: base( networkInteface ) 
		{
		}
	}
}
