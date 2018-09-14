using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared
{
	public interface IFilesQueue
	{
		bool IsOver { get; set; }
		bool IsEmpty();
	}
}
