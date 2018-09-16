using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using DirHasher.Processing;
using DirHasher.Shared;


namespace DirHasher
{
	public class DirectoriesHasher
	{
		private OsProcessor dataCollector { get; set; }
		private HashProcessor hasher { get; set; }
		private BaseProcessor baseUpdater { get; set; }

		private Thread dcThread { get; set; }
		private Thread buThread { get; set; }
		private List<Thread> hThreads { get; set; }
		private List<Thread> allThreads { get; set; }

		public DirectoriesHasher(List<string> directories, int maxHashingThreads)
		{
			var rawFilesQueue = new RawFilesQueue();
			var hashedFilesQueue = new HashedFilesQueue();

			dataCollector = new OsProcessor(rawFilesQueue, directories);
			hasher = new HashProcessor(rawFilesQueue, hashedFilesQueue);
			baseUpdater = new BaseProcessor(hashedFilesQueue);

			CreateThreads(maxHashingThreads);
		}

		private void CreateThreads(int maxHashingThreads)
		{
			dcThread = new Thread(new ThreadStart(dataCollector.Run));
			buThread = new Thread(new ThreadStart(baseUpdater.Run));
			hThreads = new List<Thread>();
			for (int i = 0; i < maxHashingThreads; ++i)
			{
				var thread = new Thread(new ThreadStart(hasher.Run));
				thread.Name = i.ToString();
				hThreads.Add(thread);
			}

			allThreads = new List<Thread>();
			allThreads.Add(dcThread);
			allThreads.AddRange(hThreads);
			allThreads.Add(buThread);
		}
		public void Run()
		{
			Console.WriteLine("Starting Data Collector...");
			dcThread.Start();

			Console.WriteLine("Starting Hashers...");
			foreach (var thread in hThreads)
				thread.Start();

			Console.WriteLine("Starting DB Updater...");
			buThread.Start();

			foreach (var thread in allThreads)
				thread.Join();

		}

	}
}
