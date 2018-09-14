using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Security.Cryptography;

using ThreadsTest.Shared;


namespace ThreadsTest.Processing
{ 
	// TODO добавить родит. класс для Hasher и DataCollector, чтобы убарть одинаковое в start
	public class Hasher
	{
		private RawFilesQueue rawFilesQueue { get; set; }
		private HashedFilesQueue hashedFilesQueue { get; set; }
		public Hasher(RawFilesQueue rawFilesQueue, HashedFilesQueue hashedFilesQueue)
		{
			this.rawFilesQueue = rawFilesQueue;
			this.hashedFilesQueue = hashedFilesQueue;
		}

		public void Start()
		{
			while (!rawFilesQueue.IsEmpty() || !rawFilesQueue.IsOver)
			{
				if (rawFilesQueue.IsEmpty()  && !rawFilesQueue.IsOver)
					Thread.Sleep(1000);
				else
				{
					var rawFile = rawFilesQueue.GetNextFile();
					var hashedFile = new HashedFileParams()
					{
						path = rawFile.path,
						hash = GetMD5(rawFile.data)
					};
					hashedFilesQueue.AddNewHashedFile(hashedFile, Thread.CurrentThread.Name);
					
				}
			}
			hashedFilesQueue.IsOver = true;
		}
		private string GetMD5(byte[] toHash)
		{
			using (var md5Hash = MD5.Create())
			{
				var md5 = md5Hash.ComputeHash(toHash);
				var sBuilder = new StringBuilder();
				for (int i = 0; i < md5.Length; i++)
					sBuilder.Append(md5[i].ToString("x2"));
				return sBuilder.ToString();
			}
		}
	}
}
