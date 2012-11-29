using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMapper
{
	public class TaskManager
	{
		public object run(object action)
		{
			Task<object> task = Task.Factory.StartNew<object>(() =>  action );
			task.Wait();
			return task.Result;
		}
	}
}
