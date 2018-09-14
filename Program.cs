using System.IO;
using System.Collections.Generic;

namespace DirHasher
{
	//TODO using на считывание с диска
	class Program
	{
		
		static void Main(string[] args)
		{
			var dirs = new List <string>(){ @"F:\HashMe\" };
			int maxHashingThreads = 2;

			var hasher = new DirectoriesHasher(dirs, maxHashingThreads);
			hasher.Run();

			
		}

	}
}
