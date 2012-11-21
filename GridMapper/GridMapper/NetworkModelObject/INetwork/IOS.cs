using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject
{
	public interface IOS
	{
		Version OsVersion { get; }
		string OsPlatform { get; }
		string OsSerialNumber { get; }
		string OsName { get; }
	}
}
