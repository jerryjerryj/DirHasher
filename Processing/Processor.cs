using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DirHasher.Shared.Base.Transactions;
using DirHasher.Shared.Base.Params;
namespace DirHasher.Processing
{
	abstract public class Processor
	{
		public virtual void Run()
		{
			try
			{
				ExternalRun();
				End();
			}
			catch (Exception e)
			{
				End();
				SendLogException(e);
			}
		}
		protected abstract void ExternalRun();
		protected abstract void End();

		private void SendLogException(Exception e)
		{
			Console.WriteLine("DEBUG log sending");
			var query = new AddLogQuery<LogParams>();
			query.Execute(new LogParams() { exceptionMessage = e.Message, timeOccured = DateTime.Now });

		}
	}
}
