using System;
using System.Collections.Generic;
using System.Threading;

namespace DirHasher.Shared
{
	public abstract class FilesQueue<TFile> : IFilesQueue
	{
		protected Queue<TFile> tFiles;
		static Mutex mutex = new Mutex();
		public bool IsOver { get; set; } = false;
		
		public FilesQueue()
		{
			tFiles = new Queue<TFile>();
		}
		public TFile GetNextFile()
		{
			mutex.WaitOne();
			var nextFile = tFiles.Dequeue();
			mutex.ReleaseMutex();

			return nextFile;
		}

		public bool IsEmpty()
		{
			return tFiles.Count == 0;
		}


	}
}
