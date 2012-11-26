using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridMapper.Repository;

namespace GridMapper
{
	public interface IExecution
	{
		Option Option { get; }
		//TaskManager TaskManager { get; }
		IRepository Repository { get; }

		void StartScan();
	}
}
