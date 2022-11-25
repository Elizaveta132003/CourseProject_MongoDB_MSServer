using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SQLServer
{
	public abstract class ReadByDataBase : Connection
	{
		public ReadByDataBase(string connectionString) 
			: base(connectionString)
		{
		}

		public void Read<T>(string sql, T value, Action<SqlDataReader, T> action)
		{
			using (var connection = Connect())
			{
				connection.Open();
				SqlCommand command = new SqlCommand(sql, connection);

				using (SqlDataReader reader = command.ExecuteReader())
				{

					while (reader.Read())
					{
						action(reader, value);
					}
				}
			}
		}
	}
}
