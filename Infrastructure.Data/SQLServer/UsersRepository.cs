using Domain.Core.Models.Roles;
using Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;


namespace Infrastructure.Data.SQLServer
{
	public class UsersRepository : ReadByDataBase, IRepository<Client>
	{

		private string _insertQuery = $"INSERT INTO Users (NameOrganization, PhoneNumber, Password) VALUES(@nameOrganization,@phoneNumber,@password)";
		private string _updateQuery = $"UPDATE Users SET NameOrganization=@nameOrganization, PhoneNumber=@phoneNumber, Password=@password where Id=@id";
		private string _deleteQuery = "Delete from Users where Users.Id=@id";
		public UsersRepository(string connectionString) : base(connectionString)
		{
		}

		public bool Create(Client item)
		{
			try
			{
				var nameOrganization = item.NameOrganization;
				var phoneNumber = item.PhoneNumber;
				var password = item.Password;

				var connect = Connect();
				connect.Open();
				string query = _insertQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@nameOrganization", SqlDbType.VarChar).Value = nameOrganization;
				command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
				command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
				int number = command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(int id)
		{
			try
			{
				var connect = Connect();
				connect.Open();
				string query = _deleteQuery;
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = id;
				command.ExecuteNonQuery();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<Client> GetAll()
		{
			List<Client> clients = new List<Client>();

			Read("SELECT * FROM Users", clients, GetClients);

			return clients;
		}
		private void GetClients(SqlDataReader reader, List<Client> clients)
		{
			int id = reader.GetInt32(0);
			string nameOrganization = reader.GetString(1);
			string phoneNumber = reader.GetString(2);
			string password = reader.GetString(3);

			var client = new Client(id, nameOrganization, phoneNumber, password);
			clients.Add(client);
		}
		private void GetClient(SqlDataReader reader, Client client)
		{
			client.Id = reader.GetInt32(0);
			client.NameOrganization = reader.GetString(1);
			client.PhoneNumber = reader.GetString(2);
			client.Password = reader.GetString(3);
		}

		public Client GetT(int id)
		{
			Client client = new Client();

			Read($"select * from Users where Id={id}", client, GetClient);
			return client;
		}

		public bool Update(Client item)
		{
			try
			{
				var id = item.Id;
				var nameOrganization = item.NameOrganization;
				var phoneNumber = item.PhoneNumber;
				var password = item.Password;

				var connect = Connect();
				connect.Open();
				string query = _updateQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.Parameters.Add("@nameOrganization", SqlDbType.VarChar).Value = nameOrganization;
				command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
				command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
				int number = command.ExecuteNonQuery();
				connect.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
