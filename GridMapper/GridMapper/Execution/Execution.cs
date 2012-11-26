using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using GridMapper.NetworkModelObject;
using GridMapper.Repository;

namespace GridMapper
{
	class Execution : IExecution
	{
		Option _option;
		IRepository _repository;


		#region IExecution Membres

		public Option Option
		{
			get { return _option; }
		}

		public IRepository Repository
		{
			get { return _repository; }
		}

		public void StartScan()
		{
			throw new NotImplementedException();
		}

		#endregion

		public Execution( Option StartupOptions )
		{
		}
	}
}
