using Domain.Core.Models;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;

namespace Infrastructure.Data.MongoDB
{
	public class StockRepository : MainMongoRepository<Stock>
	{
		public StockRepository(string connectionString) : base(connectionString, "stock")
		{
		}

		public override async Task<bool> CreateAsync(Stock item)
		{
			var parser = new MongoParser();
			item.Id = parser.MaxIndex(_mongoCollection) + 1;

			var productDocument = new BsonDocument
			{
				{"product_id", item.Product.Id },
				{"name", item.Product.Name},
				{"price", item.Product.Price},
				{"type", item.Product.Type }
			};

			var employeeDocument = new BsonDocument
			{
				{"employee_id", item.Employee.Id},
				{"phoneNumber", item.Employee.PhoneNumber},
				{"password", item.Employee.Password},
				{"firstName", item.Employee.FirstName },
				{"lastName", item.Employee.LastName },
				{"middleName", item.Employee.MiddleName },
				{"positionCode", item.Employee.PositionCode }
			};

			var document = new BsonDocument
			{
				{"dateOfReceipt", item.DateOfReceipt },
				{"dateOfDispatch", item.DateOfDispatch },
				{"orderDate", item.OrderDate },
				{"count", item.Count},
				{"product", productDocument},
				{"employee", employeeDocument }
			};

			await _mongoCollection.InsertOneAsync(document);

			return true;
		}

		public override async Task<bool> UpdateAsync(Stock item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("dateOfReceipt", item.DateOfReceipt);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("dateOfDispatch", item.DateOfDispatch);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("orderDate", item.OrderDate);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("count", item.Count);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("product", item.Product);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("employee", item.Employee);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;
		}

		protected override Stock Initialization(BsonDocument item)
			=> new Stock()
			{
				DateOfReceipt = DateTime.Parse(item.GetValue("dateOfReceipt").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				DateOfDispatch = DateTime.Parse(item.GetValue("dateOfDispatch").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				OrderDate = DateTime.Parse(item.GetValue("orderDate").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				Count = item.GetValue("count").ToInt32(),
				Product = new Product()
				{
					Id = item.GetValue("product_id").ToInt32(),
					Price = item.GetValue("price").ToDecimal(),
					Type = item.GetValue("type").ToInt32(),
					Name = item.GetValue("name").ToString()
				},
				Employee = new Employee()
				{
					Id = item.GetValue("employee_id").ToInt32(),
					PhoneNumber = item.GetValue("phoneNumber").ToString(),
					Password = item.GetValue("password").ToString(),
					FirstName = item.GetValue("firstName").ToString(),
					LastName = item.GetValue("lastName").ToString(),
					MiddleName = item.GetValue("middleName").ToString(),
					PositionCode = item.GetValue("positionCode").ToInt32()
				}
			};
	}
}
