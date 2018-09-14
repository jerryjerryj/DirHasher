using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Dapper;

namespace DirHasher.Shared.Base.Transactions
{
	//TODO не очень наследники, переделать
	public abstract class BaseQuery<TQueryParams> : Base
	{
		public void Execute(TQueryParams queryParams)
		{
			using (var oleDbConnection = CreateConnection())
			{
				var oleDbTransaction = oleDbConnection.BeginTransaction();
				try
				{
					ExecuteInternal(oleDbTransaction, queryParams);
					oleDbTransaction.Commit();
				}
				catch (Exception ex)
				{
					oleDbTransaction.Rollback();
					throw;
				}
				finally
				{
					if (oleDbConnection.State == ConnectionState.Open)
						oleDbConnection.Close();
				}
			}
			
		}

		protected abstract void ExecuteInternal(OleDbTransaction oleDbTransaction, TQueryParams queryParams);

	}
}
