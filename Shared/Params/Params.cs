using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirHasher.Shared
{
	public abstract class Params :IDisposable
	{
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
