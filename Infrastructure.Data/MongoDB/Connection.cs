using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public abstract class Connection
	{
		protected string _connectionString;

		protected Connection(string connectionString)
		{
			_connectionString = connectionString;
		}


		protected MongoClient Connect()
		{
			return new MongoClient(_connectionString);
		}
	}
}
