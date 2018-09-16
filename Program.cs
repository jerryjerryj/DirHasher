using System;
using System.Collections.Generic;

namespace DirHasher
{
	//TODO using на считывание с диска
	class Program
	{
		
		static void Main(string[] args)
		{
			var dirs = new List <string>(){ @"F:\Dope [ALAC]" };
			int hashingThreads = 3;

			var watch = System.Diagnostics.Stopwatch.StartNew();
			var hasher = new DirectoriesHasher(dirs, hashingThreads);
			hasher.Run();
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			Console.WriteLine(elapsedMs);
			
			Console.WriteLine("Press any key ");
			Console.ReadKey();


		}

	}
}
