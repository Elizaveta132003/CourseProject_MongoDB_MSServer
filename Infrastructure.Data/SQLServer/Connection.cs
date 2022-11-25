using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SQLServer
{
	public abstract class Connection
	{
		protected string _connectionString;

		protected Connection(string connectionString)
		{
			_connectionString = connectionString;
		}

		//public Connection()
		//	:this(@"Server=(localdb)\mssqllocaldb;Database=Kursovaya;Trusted_Connection=True;")
		//{
		
		//}

		protected SqlConnection Connect()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
