using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace ThreadsTest.Shared.Base.Selections
{
	public abstract class BaseSelect<TQueryParams, TQueryResult> : Base
	{
		public TQueryResult Execute(TQueryParams queryParams)
		{
			using (var oleDbConnection = CreateConnection())
			{
				try
				{
					return SelectInternal(oleDbConnection, queryParams);
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					if (oleDbConnection.State == ConnectionState.Open)
						oleDbConnection.Close();
				}
			}
		}

		protected abstract TQueryResult SelectInternal(OleDbConnection oleDbConnection, TQueryParams queryParams);

	}
}
