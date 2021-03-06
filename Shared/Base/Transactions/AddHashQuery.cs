﻿
using DirHasher.Shared.Base.Params;
using System.Data.OleDb;
using Dapper;
namespace DirHasher.Shared.Base.Transactions
{
	class AddHashQuery<TQueryParams> : BaseQuery<TQueryParams> where TQueryParams : HashParams
	{
		protected override void ExecuteInternal(OleDbTransaction oleDbTransaction, TQueryParams queryParams)
		{
			string query = "INSERT INTO hashes (path, hash, scan_id) Values(?Path?,?Hash?,?ScanId?)";
			oleDbTransaction.Connection.Execute(query,
				new { Path = queryParams.path,
					Hash = queryParams.hash,
					ScanId = queryParams.scanId
				}, oleDbTransaction);
		}
	}
}
