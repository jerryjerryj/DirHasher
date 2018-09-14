using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsTest.Shared
{
	 public class HashedFilesQueue :FilesQueue<HashedFileParams>
	{
		public void AddNewHashedFile(HashedFileParams hashedFile, string threadName)
		{
			tFiles.Enqueue(hashedFile);
			
		}
	}
}
