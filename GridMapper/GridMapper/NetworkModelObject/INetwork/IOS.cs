using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject
{
	public interface IOS
	{
		String OsVersion { get; }
		string OsCpu { get; }
		string OsRam { get; }
		string OsComputerModel { get; }
		string OsSerialNumber { get; }
		string OsName { get; }
		string OsAssetTag { get; }
	}
}
