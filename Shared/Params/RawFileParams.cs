﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared
{
	public class RawFileParams: Params
	{
		public byte[] data { get; set; }
		public string path { get; set; }
	}
}
