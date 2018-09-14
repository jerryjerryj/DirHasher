using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;
using DirHasher.Shared;

namespace DirHasher.Processing
{
	public class OsProcessor
	{
		private List<string> directories { get; set; }
		private RawFilesQueue rawFilesQueue { get; set; }

		public OsProcessor(RawFilesQueue rawFilesQueue, List<string> directories)
		{
			this.rawFilesQueue = rawFilesQueue;
			this.directories = directories;
		}

		public void Run()
		{	
			foreach (var directory in directories)
				UpdateQueueFromDir(directory);
			rawFilesQueue.IsOver = true;
		}

		private void UpdateQueueFromDir(string currentDirectory)
		{
			var hashableFiles = GetFiles(currentDirectory);
			rawFilesQueue.AddNewHashableFiles(hashableFiles);

			var directories = Directory.GetDirectories(currentDirectory);
			foreach (var directory in directories)
			{
				UpdateQueueFromDir(directory);
			}
		}
		private List<RawFileParams> GetFiles(string currentDirectory)
		{
			var files = Directory.GetFiles(currentDirectory);
			var hashableFiles = new List<RawFileParams>();
			foreach (var filePath in files)
			{
				byte[] fileBytes = File.ReadAllBytes(filePath);
				hashableFiles.Add(new RawFileParams(){
					data = fileBytes,
					path = filePath
				});
			}
			return hashableFiles;
		}
	}
}
