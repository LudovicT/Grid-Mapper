using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GridMapper.NetworkModelObject
{
    //need info
	public interface ISwitch : INetworkItem
	{
		HashSet<ICAMdata> CAMTable { get; }
		ReadOnlyCollection<INetworkItem> NetworkItems { get; }
	}
}
