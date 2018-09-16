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
		protected override void ExternalRun()
		{
			currentScanId = StartNewScan(); //TODO переделать, чтобы было видно что ID передаётся далее в InsertNextHashToBase
			RunWithQueueInteractions(InsertNextHashToBase);
		}

		protected override void End() { }

		private void InsertNextHashToBase()
		{
			using (var hashedFile = dependOfQueue.GetNextFile())
			{
				var query = new AddHashQuery<HashParams>();
				query.Execute(new HashParams() { hash = hashedFile.hash, path = hashedFile.path, scanId = currentScanId });
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
