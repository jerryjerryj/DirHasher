using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;
using ThreadsTest.Shared;

namespace ThreadsTest.Processing
{
	public class DataCollector
	{
		private List<string> directories { get; set; }
		private RawFilesQueue rawFilesQueue { get; set; }

		public DataCollector(RawFilesQueue rawFilesQueue, List<string> directories)
		{
			this.rawFilesQueue = rawFilesQueue;
			this.directories = directories;
		}

		public void Start()
		{	
			foreach (var directory in directories)
				Search(directory);
			rawFilesQueue.IsOver = true;
		}

		private void Search(string current_directory)
		{

			var files = Directory.GetFiles(current_directory);
			var hashableFiles = new List<RawFileParams>();
			foreach (var file_path in files)
			{
				byte[] file_bytes = File.ReadAllBytes(file_path);
				hashableFiles.Add(new RawFileParams() {
					data = file_bytes,
					path = file_path
				});
			}
			rawFilesQueue.AddNewHashableFiles(hashableFiles);

			var directories = Directory.GetDirectories(current_directory);
			foreach (var directory in directories)
			{
				Search(directory);
			}
		}
	}
}
