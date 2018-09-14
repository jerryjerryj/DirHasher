using System;
using System.Threading;

using DirHasher.Shared;

namespace DirHasher.Processing
{
	abstract public class Dependent<TDependOfQueue>
		where TDependOfQueue: IFilesQueue
	{
		private readonly int TimeOut = 1000;

		protected TDependOfQueue dependOfQueue { get; set; }

		public void RunInProcessQueues(Action callback)
		{
			while (!dependOfQueue.IsEmpty() || !dependOfQueue.IsOver)
			{
				if (dependOfQueue.IsEmpty() && !dependOfQueue.IsOver)
					Thread.Sleep(TimeOut);
				else
				{
					callback.Invoke();
				}
			}
		}

	}
}
