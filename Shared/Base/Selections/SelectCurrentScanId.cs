using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using Dapper;
using ThreadsTest.Shared.Base.Params;

namespace ThreadsTest.Shared.Base.Selections
{
	class SelectCurrentScanId : BaseSelect<ScanParams, int>
	{
		protected override int SelectInternal(OleDbConnection mySqlConnection, ScanParams queryParams)
		{
			string query = "SELECT max(id) from scans";
			return mySqlConnection.Query<int>(query).SingleOrDefault();
		}
	}
}
