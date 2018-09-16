using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared
{
	public class HashedFileParams : Params
	{
		public string path { get; set; }
		public string hash { get; set; }
	}
}
