using System;
using DirHasher.Shared;

using DirHasher.Shared.Base.Transactions;
using DirHasher.Shared.Base.Selections;
using DirHasher.Shared.Base.Params;
namespace DirHasher.Processing
{
	public class BaseProcessor : Dependent<HashedFilesQueue>
	{
		private int currentScanId { get; set; }
		public BaseProcessor(HashedFilesQueue hashedFilesQueue)
		{
			this.dependOfQueue = hashedFilesQueue;
		}
		public void Run()
		{
			currentScanId = StartNewScan();
			RunInProcessQueues(InsertNextHashToBase);
		}
		private void InsertNextHashToBase()
		{
			var hashedFile = dependOfQueue.GetNextFile();
			var query = new AddHashQuery<HashParams>();
			query.Execute(new HashParams() { hash = hashedFile.hash, path = hashedFile.path, scanId = currentScanId });
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
