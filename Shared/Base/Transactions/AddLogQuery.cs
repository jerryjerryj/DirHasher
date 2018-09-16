using DirHasher.Shared.Base.Params;
using System.Data.OleDb;
using Dapper;
namespace DirHasher.Shared.Base.Transactions
{
	class AddLogQuery<TQueryParams> : BaseQuery<TQueryParams> where TQueryParams : LogParams
	{
		protected override void ExecuteInternal(OleDbTransaction oleDbTransaction, TQueryParams queryParams)
		{
			string query = "INSERT INTO logs (exception, occured) Values(?Exception?,?Occured?)";
			oleDbTransaction.Connection.Execute(query,
				new
				{
					Exception = queryParams.exceptionMessage,
					Occured = queryParams.timeOccured
				}, oleDbTransaction);
		}
	}
}
