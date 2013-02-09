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
		StreamWriter _streamWriter;

		public SaveTextFile(string path)
		{
			_path = path;
			StreamWriter _streamWriter = new StreamWriter( _path );
		} 

		public SaveTextFile( StreamWriter streamWriter )
		{
			_path = string.Empty;
			StreamWriter _streamWriter = streamWriter;
		}

		public void write()
		{
			_streamWriter.WriteLine( "Network :" );
			foreach( HostEntity hostEntity in Network.HostsEntities.Values )
			{
				hostEntity.Describe( _streamWriter );
			}
		}
	}
}
