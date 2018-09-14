using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared
{
	public class RawFilesQueue : FilesQueue<RawFileParams>
	{
		public void AddNewHashableFiles(List<RawFileParams> files)
		{
			foreach (var file in files)
				tFiles.Enqueue(file);
			
		}
	}
}
