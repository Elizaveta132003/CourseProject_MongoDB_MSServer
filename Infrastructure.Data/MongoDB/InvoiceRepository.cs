using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;

namespace Infrastructure.Data.MongoDB
{
	public class InvoiceRepository : MainMongoRepository<Invoice>
	{
		private OrderItemsGelAllById _orderItemsGetAllById;
		public InvoiceRepository(string connectionString) : base(connectionString, "invoices")
		{
			_orderItemsGetAllById = new OrderItemsGelAllById(_mongoCollection);
		}

		public override async Task<bool> CreateAsync(Invoice item)
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
				{"middleName", item.Employee.MiddleName }
			};

			var orderItem = new BsonArray();
			item.Order.Orders.ForEach(item =>
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

			var orderDocument = new BsonDocument
			{
				{"order_id", item.Order.Id},
				{"products", orderItem},
				{"client", clientDocument},
				{"orderDate", item.Order.OrderDate },
				{"dateOfSqipment", item.Order.DateOfSqipment },
				{"status", item.Order.Status },
				{"price", item.Order.Price },
				{"employee", employeeDocument },
				{"street", item.Order.Street },
				{"houseNumber", item.Order.HouseNumber }
			};

			var document = new BsonDocument
			{
				{"_id", item.Id },
				{"registrationDate", item.RegistrationDate },
				{"nameOrganization", item.NameOrganization },
				{"street", item.Street },
				{"houseNumber", item.HouseNumber },
				{"order", orderDocument},
				{"employee", employeeDocument },
				{"user", clientDocument}
			};

			await _mongoCollection.InsertOneAsync(document);

			return true;
		}

		public override async Task<bool> UpdateAsync(Invoice item)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("_id", item.Id);

			var update = Builders<BsonDocument>.Update.Set("registrationDate", item.RegistrationDate);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("nameOrganization", item.NameOrganization);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("street", item.Street);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("houseNumber", item.HouseNumber);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("order", item.Order);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("employee", item.Employee);
			await _mongoCollection.UpdateOneAsync(filter, update);

			update = Builders<BsonDocument>.Update.Set("user", item.Client);
			await _mongoCollection.UpdateOneAsync(filter, update);

			return true;

		}

		protected override Invoice Initialization(BsonDocument item)
			=> new Invoice()
			{
				Id = item.GetValue("_id").ToInt32(),
				RegistrationDate = DateTime.Parse(item.GetValue("registrationDate").ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				NameOrganization = item.GetValue("nameOrganization").ToString(),
				Street = item.GetValue("street").ToString(),
				HouseNumber = item.GetValue("houseNumber").ToString(),
				Order = GetOrder(item),
				Employee = new Employee()
				{
					Id = item.GetValue("employee")["employee_id"].ToInt32(),
					PhoneNumber = item.GetValue("employee")["phoneNumber"].ToString(),
					LastName = item.GetValue("employee")["lastName"].ToString(),
					MiddleName = item.GetValue("employee")["middleName"].ToString(),
					FirstName = item.GetValue("employee")["firstName"].ToString()
				},
				Client = new Client()
				{
					Id = item.GetValue("user")["client_id"].ToInt32(),
					NameOrganization = item.GetValue("user")["nameOrganization"].ToString(),
					PhoneNumber = item.GetValue("user")["phoneNumber"].ToString(),
					Password = null
				}
			};

		private Order GetOrder(BsonDocument item)
		{
			return new Order()
			{
				Id = item.GetValue("order")["order_id"].ToInt32(),
				Client = new Client()
				{
					Id = item.GetValue("user")["client_id"].ToInt32(),
					NameOrganization = item.GetValue("user")["nameOrganization"].ToString(),
					PhoneNumber = item.GetValue("user")["phoneNumber"].ToString(),
					Password = null
				},
				OrderDate = DateTime.Parse(item.GetValue("order")["orderDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				DateOfSqipment = DateTime.Parse(item.GetValue("order")["dateOfSqipment"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
				Status = item.GetValue("order")["status"].ToString(),
				Orders = _orderItemsGetAllById.GetAllByIdOneToMany(item.GetValue("order")["order_id"].ToInt32()).Result,
				Price = item.GetValue("order")["price"].ToDecimal(),
				Employee = new Employee()
				{
					Id = item.GetValue("employee")["employee_id"].ToInt32(),
					PhoneNumber = item.GetValue("employee")["phoneNumber"].ToString(),
					LastName = item.GetValue("employee")["lastName"].ToString(),
					MiddleName = item.GetValue("employee")["middleName"].ToString(),
					FirstName = item.GetValue("employee")["firstName"].ToString()
				},
				Street = item.GetValue("order")["street"].ToString(),
				HouseNumber = item.GetValue("order")["houseNumber"].ToString()
			};
		}
	}
}
