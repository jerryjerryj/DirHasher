using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;



namespace ThreadsTest.Shared.Base
{
	 public class Base
	{
		private string ConnectionStr = "Provider=MSDAORA;Data Source=orcl;User ID=system;Password=123456Qwerty;Unicode=True";
		protected OleDbConnection CreateConnection()
		{
			var oleDbConnection = new OleDbConnection(ConnectionStr);
			oleDbConnection.Open();
			return oleDbConnection;
		}
	}
}
