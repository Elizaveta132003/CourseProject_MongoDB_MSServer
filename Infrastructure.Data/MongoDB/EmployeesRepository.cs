using Domain.Core.Models.Roles;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.MongoDB
{
	public class EmployeesRepository : MainMongoRepository<Employee>
	{
		public EmployeesRepository(string connectionString) : base(connectionString, "employees")
		{
		}

		public override async Task<bool> CreateAsync(Employee item)
		{
			var parser = new MongoParser();
			item.Id = parser.MaxIndex(_mongoCollection) + 1;

			var document = new BsonDocument
			{
				{"_id",item.Id},
				{"phoneNumber", item.PhoneNumber},
				{"password", item.Password},
				{"firstName", item.FirstName },
				{"lastName", item.LastName },
				{"middleName", item.MiddleName },
				{"positionCode", item.PositionCode }
			};

			await _mongoCollection.InsertOneAsync(document);

			return true;
		}

		public override async Task<bool> UpdateAsync(Employee item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("phoneNumber", item.PhoneNumber);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("password", item.Password);
			await _mongoCollection.UpdateOneAsync(filter, update);


			update = Builders<BsonDocument>.Update.Set("firstName", item.FirstName);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("lastName", item.LastName);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("middleName", item.MiddleName);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("positionCode", item.PositionCode);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;
		}

		protected override Employee Initialization(BsonDocument item)
			=> new Employee()
			{
				Id = item.GetValue("_id").ToInt32(),
				PhoneNumber = item.GetValue("phoneNumber").ToString(),
				Password = item.GetValue("password").ToString(),
				LastName = item.GetValue("lastName").ToString(),
				FirstName = item.GetValue("firstName").ToString(),
				MiddleName = item.GetValue("middleName").ToString(),
				PositionCode = item.GetValue("positionCode").ToInt32()
			};
	}
}
