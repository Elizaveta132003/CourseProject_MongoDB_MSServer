using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Infrastructure.Data.MongoDB
{
	public class OrdersRepository : MainMongoRepository<Order>
	{
		private OrderItemsGelAllById _orderItemsGetAllById;
		public OrdersRepository(string connectionString) : base(connectionString, "orders")
		{
			_orderItemsGetAllById = new OrderItemsGelAllById(_mongoCollection);
		}

		public override async Task<bool> CreateAsync(Order item)
		{
			var parser = new MongoParser();
			item.Id = parser.MaxIndex(_mongoCollection) + 1;


			var clientDocument = new BsonDocument
			{
				{"client_id", item.Client.Id },
				{"nameOrganization", item.Client.NameOrganization },
				{"phoneNumber", item.Client.PhoneNumber }
			};

			var employeeDocument = new BsonDocument
			{
				{"employee_id", item.Employee.Id},
				{"phoneNumber", item.Employee.PhoneNumber },
				{"lastName", item.Employee.LastName },
				{"firstName", item.Employee.FirstName },
				{"middleName", item.Employee.MiddleName },
				{"positionCode", item.Employee.PositionCode}
			};

			var orderItem = new BsonArray();
			item.Orders.ForEach(item =>
			{
				orderItem.Add(new BsonDocument
				{
					{"orderItem_id", item.Product.Id },
					{"productName", item.Product.Name },
					{"productPrice", item.Product.Price },
					{"productType", item.Product.Type },
					{"count", item.Count}
				});
			});

			var document = new BsonDocument
			{
				{"_id", item.Id},
				{"products", orderItem},
				{"client", clientDocument},
				{"orderDate", item.OrderDate },
				{"dateOfSqipment", item.DateOfSqipment },
				{"status", "Informulated" },
				{"price", item.Price },
				{"employee", employeeDocument },
				{"street", item.Street },
				{"houseNumber", item.HouseNumber }
			};

			await _mongoCollection.InsertOneAsync(document);

			return true;

		}


		public override async Task<bool> UpdateAsync(Order item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("products", item.Orders);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("client", item.Client);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("orderDate", item.OrderDate);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("dateOfSqipment", item.DateOfSqipment);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("status", item.Status);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("price", item.Price);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("employee", item.Employee);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("street", item.Street);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("houseNumber", item.HouseNumber);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;
		}

		protected override Order Initialization(BsonDocument item)
			=> new Order()
			{
				Id = item.GetValue("_id").ToInt32(),
				Client = new Client()
				{
					Id = item.GetValue("client")["client_id"].ToInt32(),
					NameOrganization = item.GetValue("client")["nameOrganization"].ToString(),
					PhoneNumber = item.GetValue("client")["phoneNumber"].ToString(),
					Password = null
				},
				OrderDate = DateTime.Parse(item.GetValue("orderDate").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				DateOfSqipment = DateTime.Parse(item.GetValue("dateOfSqipment").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				Status = item.GetValue("status").ToString(),
				Orders = _orderItemsGetAllById.GetAllByIdOneToMany(item.GetValue("_id").ToInt32()).Result,
				Price = item.GetValue("price").ToDecimal(),
				Employee = new Employee()
				{
					Id = item.GetValue("employee")["employee_id"].ToInt32(),
					PhoneNumber = item.GetValue("employee")["phoneNumber"].ToString(),
					LastName = item.GetValue("employee")["lastName"].ToString(),
					MiddleName = item.GetValue("employee")["middleName"].ToString(),
					FirstName = item.GetValue("employee")["firstName"].ToString(),
					PositionCode = item.GetValue("employee")["positionCode"].ToInt32()
				},

				Street = item.GetValue("street").ToString(),

				HouseNumber = item.GetValue("houseNumber").ToString()

			};
		//{
		//	var order = new Order();
		//	order.Id = item.GetValue("_id").ToInt32();
		//	var Client = new Client();

		//	Client.Id = item.GetValue("client")["client_id"].ToInt32();
		//	Client.NameOrganization = item.GetValue("client")["nameOrganization"].ToString();
		//	Client.PhoneNumber = item.GetValue("client")["phoneNumber"].ToString();
		//	Client.Password = item.GetValue("client")["password"].ToString();
		//	order.Client = Client;
		//	order.OrderDate = DateTime.Parse(item.GetValue("orderDate").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
		//	order.DateOfSqipment = DateTime.Parse(item.GetValue("dateOfSqipment").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
		//	order.Status = item.GetValue("status").ToString();
		//	order.Orders = _orderItemsGetAllById.GetAllByIdOneToMany(item.GetValue("_id").ToInt32()).Result;
		//	order.Price = item.GetValue("price").ToDecimal();
		//	var Employee = new Employee()
		//	{
		//		Id = item.GetValue("employee")["employee_id"].ToInt32(),
		//		PhoneNumber = item.GetValue("employee")["phoneNumber"].ToString(),
		//		LastName = item.GetValue("employee")["lastName"].ToString(),
		//		MiddleName = item.GetValue("employee")["middleName"].ToString(),
		//		FirstName = item.GetValue("employee")["firstName"].ToString(),
		//		PositionCode = item.GetValue("employee")["positionCode"].ToInt32()
		//	};
		//	order.Employee = Employee;
		//	order.Street = item.GetValue("street").ToString();
		//	order.HouseNumber = item.GetValue("houseNumber").ToString();

		//	return order;
		//}

	}
}
