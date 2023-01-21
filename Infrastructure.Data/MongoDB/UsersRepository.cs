using Domain.Core.Models.Roles;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class UsersRepository : MainMongoRepository<Client>
	{
		public UsersRepository(string connectionString) : base(connectionString, "users")
		{
		}

		public override async Task<bool> CreateAsync(Client item)
		{
			var parser = new MongoParser();
			item.Id = parser.MaxIndex(_mongoCollection) + 1;

			var document = new BsonDocument
			{
				{"_id",item.Id},
				{"nameOrganization", item.NameOrganization},
				{"password", item.Password},
				{"phoneNumber", item.PhoneNumber }
			};

			await _mongoCollection.InsertOneAsync(document);

			return true;
		}

		public override async Task<bool> UpdateAsync(Client item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("nameOrganization", item.NameOrganization);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("password", item.Password);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("phoneNumber", item.PhoneNumber);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;
		}

		protected override Client Initialization(BsonDocument item)
			=> new Client()
			{
				Id = item.GetValue("_id").ToInt32(),
				NameOrganization = item.GetValue("nameOrganization").ToString(),
				Password = item.GetValue("password").ToString(),
				PhoneNumber = item.GetValue("phoneNumber").ToString()
			};
	}
}
