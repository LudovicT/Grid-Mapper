﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheckNetworks.NetworkModelObject
{
	abstract class  Entity
	{
		IList<INetworkInterface> _networkInterfaces;
	}
}
