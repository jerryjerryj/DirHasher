using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ThreadsTest.Shared;

using ThreadsTest.Shared.Base.Transactions;
using ThreadsTest.Shared.Base.Selections;
using ThreadsTest.Shared.Base.Params;
namespace ThreadsTest.Processing
{
	public class BaseUpdater
	{
		private HashedFilesQueue hashedFilesQueue { get; set; }
		public BaseUpdater(HashedFilesQueue hashedFilesQueue)
		{
			this.hashedFilesQueue = hashedFilesQueue;
		}
		public void Start()
		{
			var currentScanId = StartNewScan();
			while (!hashedFilesQueue.IsEmpty() || !hashedFilesQueue.IsOver)
			{
				if (hashedFilesQueue.IsEmpty() && !hashedFilesQueue.IsOver)
					Thread.Sleep(1000);
				else
				{
					var hashedFile = hashedFilesQueue.GetNextFile();
					var query = new AddHashQuery<HashParams>();
					query.Execute(new HashParams() { hash = hashedFile.hash, path = hashedFile.path, scanId = currentScanId });
					
				}
			}			
		}
		private int StartNewScan()
		{
			var query = new AddScanQuery<ScanParams>();
			query.Execute(new ScanParams() { startTime = DateTime.Now });

			var currentScanQuery = new SelectCurrentScanId();
			return (int)currentScanQuery.Execute(null);

		}
	}
}
