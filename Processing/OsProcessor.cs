using System.IO;
using System;
using System.Collections.Generic;
using DirHasher.Shared;

namespace DirHasher.Processing
{
	public class OsProcessor :Processor
	{
		private List<string> directories { get; set; }
		private RawFilesQueue rawFilesQueue { get; set; }

		public OsProcessor(RawFilesQueue rawFilesQueue, List<string> directories)
		{
			this.rawFilesQueue = rawFilesQueue;
			this.directories = directories;
		}

		protected override void  ExternalRun()
		{
			foreach (var directory in directories)
				UpdateQueueFromDir(directory);
		}

		protected override void End()
		{
			rawFilesQueue.IsOver = true;
		}

		private void UpdateQueueFromDir(string currentDirectory)
		{
			var hashableFiles = GetFiles(currentDirectory);
			rawFilesQueue.AddNewHashableFiles(hashableFiles);

			var directories = Directory.GetDirectories(currentDirectory);
			foreach (var directory in directories)
				UpdateQueueFromDir(directory);
		}
		private List<RawFileParams> GetFiles(string currentDirectory)
		{
			var files = Directory.GetFiles(currentDirectory);
			var hashableFiles = new List<RawFileParams>();
			foreach (var filePath in files)
			{
				var fileBytes = File.ReadAllBytes(filePath);
				hashableFiles.Add(new RawFileParams()
				{
					data = fileBytes,
					path = filePath
				});
				
			}
			return hashableFiles;
		}
	}
}
