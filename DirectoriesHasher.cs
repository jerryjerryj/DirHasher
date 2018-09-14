using System;
using System.Collections.Generic;
using System.Threading;

using ThreadsTest.Processing;
using ThreadsTest.Shared;


namespace ThreadsTest
{
	public class DirectoriesHasher
	{
		private DataCollector dataCollector { get; set; }
		private Hasher hasher { get; set; }
		private BaseUpdater baseUpdater{ get; set; }

		private Thread dcThread { get; set; }
		private Thread buThread { get; set; }
		private List<Thread> hThreads { get; set; }

		public DirectoriesHasher(List<string> directories, int maxHashingThreads)
		{
			var rawFilesQueue = new RawFilesQueue();
			var hashedFilesQueue = new HashedFilesQueue();

			dataCollector = new DataCollector(rawFilesQueue, directories);
			hasher = new Hasher(rawFilesQueue, hashedFilesQueue);
			baseUpdater = new BaseUpdater(hashedFilesQueue);

			CreateThreads(maxHashingThreads);
		}

		private void CreateThreads(int maxHashingThreads)
		{
			dcThread = new Thread(new ThreadStart(dataCollector.Start));
			buThread = new Thread(new ThreadStart(baseUpdater.Start));
			hThreads = new List<Thread>();
			for (int i = 0; i < maxHashingThreads; ++i)
			{
				var thread = new Thread(new ThreadStart(hasher.Start));
				thread.Name = i.ToString();
				hThreads.Add(thread);
			}
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

			while (buThread.ThreadState != ThreadState.Stopped)
				Thread.Sleep(1000);
			Console.WriteLine("Done");
			Console.ReadKey();


		}
	}
}
