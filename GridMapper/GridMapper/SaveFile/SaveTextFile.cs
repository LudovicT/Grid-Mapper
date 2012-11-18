using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.NetworkModelObject;
using System.IO;

namespace GridMapper.SaveFile
{
	public class SaveTextFile
	{
		string _path;

		public SaveTextFile(string path)
		{
			_path = path;
		}

		public void write()
		{
			StreamWriter streamWriter = new StreamWriter( _path );
			foreach( HostEntity hostEntity in Network.HostsEntities.Values )
			{
				hostEntity.Describe( streamWriter );
			}
		}
	}
}
