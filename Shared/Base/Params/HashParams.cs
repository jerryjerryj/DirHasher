﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared.Base.Params
{
	internal class HashParams
	{
		public string path { get; set; }
		public string hash { get; set; }
		public int scanId { get; set; }
	}
}
