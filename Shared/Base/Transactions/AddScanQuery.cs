using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThreadsTest.Shared.Base.Params;
using System.Data.OleDb;
using Dapper;

namespace ThreadsTest.Shared.Base.Transactions
{
	class AddScanQuery<TQueryParams>:BaseQuery<TQueryParams> where TQueryParams: ScanParams
	{
		protected override void ExecuteInternal(OleDbTransaction oleDbTransaction, TQueryParams queryParams)
		{
			string query = "INSERT INTO scans (started) Values(?Date?)";
			oleDbTransaction.Connection.Execute(query, new { Date = queryParams.startTime }, oleDbTransaction);
		}
	}

}
