using System.Text;
using System.Threading;
using System;

using System.Security.Cryptography;

using DirHasher.Shared;



namespace DirHasher.Processing
{ 
	public class HashProcessor : Dependent<RawFilesQueue>
	{
		private HashedFilesQueue hashedFilesQueue { get; set; }
		

		public HashProcessor(RawFilesQueue rawFilesQueue, HashedFilesQueue hashedFilesQueue)
		{
			this.dependOfQueue = rawFilesQueue;
			this.hashedFilesQueue = hashedFilesQueue;
		}

		protected  override void ExternalRun()
		{
			RunWithQueueInteractions(HashNextFileInQueue);
		}

		protected override void End()
		{
			hashedFilesQueue.IsOver = true;
		}

		private void HashNextFileInQueue()
		{
			using (var rawFile = dependOfQueue.GetNextFile())
			{
				var hashedFile = new HashedFileParams()
				{
					path = rawFile.path,
					hash = GetMD5(rawFile.data)
				};
				hashedFilesQueue.AddNewHashedFile(hashedFile, Thread.CurrentThread.Name);
			}

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
