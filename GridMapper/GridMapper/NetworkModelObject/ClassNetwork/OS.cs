using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridMapper.NetworkModelObject.ClassNetwork
{
	class OS : IOS
	{
		string _osVersion;
		string _osCpu;
		string _osRam;
		string _osComputerModel;
		string _osSerialNumber;
		string _osName;
		string _osAssetTag;

		#region IOS Membres

		public string OsVersion
		{
			get { throw new NotImplementedException(); }
		}

		public string OsCpu
		{
			get { throw new NotImplementedException(); }
		}

		public string OsRam
		{
			get { throw new NotImplementedException(); }
		}

		public string OsComputerModel
		{
			get { throw new NotImplementedException(); }
		}

		public string OsSerialNumber
		{
			get { throw new NotImplementedException(); }
		}

		public string OsName
		{
			get { throw new NotImplementedException(); }
		}

		public string OsAssetTag
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		public OS(string osVersion, string osCpu, string osRam, string osComputerModel, string osSerialNumber, string osName, string osAssetTag)
		{
			_osVersion = osVersion;
			_osCpu = osCpu;
			_osRam = osRam;
			_osComputerModel = osComputerModel;
			_osSerialNumber = osSerialNumber;
			_osName = osName;
			_osAssetTag = osAssetTag;
		}
	}
}
