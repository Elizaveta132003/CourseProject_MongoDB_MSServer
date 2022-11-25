using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data.SQLServer
{
	public class OrdersRepository : ReadByDataBase,IRepository<Order>, IOrdersRepository
	{
		private string _insertQuery= @$"INSERT INTO Orders (IdClient,OrderDate,DateOfSgipment,Status, Price, Street, HouseNumber, IdEmployee)
                                        VALUES(@idClient, @orderDate, @dateOfSgipment,@status,@price, @street, @houseNumber, @idEmployee)";
		private string _insertQueryIntoOrderProducts= @$"INSERT INTO OrderProducts (IdOrder,IdProduct,QuantityProducts) 
                                                         VALUES(@idOrder,@idProduct,@qiantityProducts)";
		private string _updateQuery= @$"UPDATE Orders SET Id=@id, IdClient=@idClient, OrderDate=@orderDate, DateOfSgipment=@dateOfSgipment,
                                  Status = @status, Price=@price, IdEmployee=@idEmployee, Street=@street, HouseNumber=@houseNumber where Id=@id";
		private string _getAll = @"select Orders.Id, Users.Id, Users.NameOrganization, Users.PhoneNumber, Users.Password, 
									Orders.OrderDate, Orders.DateOfSgipment, Orders.Status,
									Orders.Price, Employees.Id, Employees.PhoneNumber, Employees.Password, Employees.LastName,
									Employees.FirstName, Employees.MiddleName, Employees.PositionCode,
									Orders.Street, Orders.HouseNumber 
									from Orders	
									inner join Users 
									on Orders.IdClient=Users.Id
									inner join Employees 
									on Employees.Id=Orders.IdEmployee ";
		private string _deleteQuery = @"Delete from Orders where Orders.Id=@id";
			 //@$"select Orders.Id, Users.Id, Users.NameOrganization, Users.PhoneNumber, Users.Password, 
			 //                        Orders.OrderDate, Orders.DateOfSgipment, Orders.Status,
			 //                        Orders.Price, Employees.Id, Employees.PhoneNumber, Employees.Password, Employees.LastName,
			 //                        Employees.FirstName, Employees.MiddleName, Employees.PositionCode,
			 //                        Orders.Street, Orders.HouseNumber 
			 //			from Orders	
			 //			inner join Users 
			 //			on Orders.IdClient=Users.Id
			 //                        inner join OrderProducts 
			 //			on Orders.Id=OrderProducts.IdOrder
			 //                        inner join Employees 
			 //			on Employees.Id=Orders.IdEmployee 
			 //			inner join Products 
			 //			on OrderProducts.IdProduct=Products.Id
			 //                        inner join TypeOfProducts 
			 //                        on TypeOfProducts.Id=Products.ProductTypeCode";

		public OrdersRepository(string connectionString) : base(connectionString)
		{
		}

		public bool Create(Order item)
		{
			try
			{
				var connect = Connect();
				connect.Open();
				string query = _insertQuery;
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@idClient", SqlDbType.Int).Value = item.Client.Id;
				command.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = DateTime.Now;
				command.Parameters.Add("@dateOfSgipment", SqlDbType.DateTime).Value = item.DateOfSqipment;
				command.Parameters.Add("@status", SqlDbType.VarChar).Value = "Unformulated";
				command.Parameters.Add("@price", SqlDbType.Decimal).Value = item.Price;
				command.Parameters.Add("@street", SqlDbType.VarChar).Value = item.Street;
				command.Parameters.Add("@houseNumber", SqlDbType.VarChar).Value = item.HouseNumber;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = item.Employee.Id;
				int number = command.ExecuteNonQuery();

				for (int i = 0; i < item.Orders.Count; i++)
				{
					string sql = _insertQueryIntoOrderProducts; 
					SqlCommand sqlCommand = new SqlCommand(sql, connect);
					sqlCommand.Parameters.Add("@idOrder", SqlDbType.Int).Value = GetAll()[GetAll().Count - 1].Id;
					sqlCommand.Parameters.Add("@idProduct", SqlDbType.Int).Value = item.Orders[i].Product.Id;
					sqlCommand.Parameters.Add("@qiantityProducts", SqlDbType.Int).Value = item.Orders[i].Count;
					int count = sqlCommand.ExecuteNonQuery();
				}

				connect.Close();

				return true;
			}
			catch
			{
				return false;
			}
		}
		

		public bool Delete(Order item)
		{
			try
			{
				var connect = Connect();
				connect.Open();
				string query = _deleteQuery;
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.ExecuteNonQuery();

				return true;
			}
			catch
			{
				return false;
			}

		}
		private int GetId(int idClient, decimal price)
		{
			int id = 0;
			Read($"select Orders.Id from Orders where Orders.IdClient={idClient} and Orders.Price={price}", id, Get);

			return id;
		}
		private void Get(SqlDataReader reader, int id)
		{
			id = reader.GetInt32(0);
		}
		

		public List<Order> GetAll()
		{
			List<Order> list = new List<Order>();
			Read(_getAll, list, GetAllOrders);
			return list;
		}
		private void GetAllOrders(SqlDataReader reader, List<Order> list)
		{
			var order = new Order();
			order.Id = reader.GetInt32(0);
			var client = new Client(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
			order.Client = client;
			order.OrderDate = reader.GetDateTime(5);
			order.DateOfSqipment = reader.GetDateTime(6);
			order.Status = reader.GetString(7);
			order.Orders = GetItems(reader.GetInt32(0));
			order.Price = reader.GetDecimal(8);
			var employee = new Employee(reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13),
				reader.GetString(14), reader.GetInt32(15))
				
				;
			order.Employee = employee;
			order.Street = reader.GetString(16);
			order.HouseNumber = reader.GetString(17);

			list.Add(order);

		}
		

		public Order GetT(int id)
		{
			var order = new Order();

			string query = @$"select Orders.Id, Users.Id, Users.NameOrganization, Users.PhoneNumber, Users.Password, 
                            Orders.OrderDate, Orders.DateOfSgipment, Orders.Status,
                            Orders.Price, Employees.Id, Employees.PhoneNumber, Employees.Password, Employees.LastName,
                            Employees.FirstName, Employees.MiddleName, Employees.PositionCode,
                            Orders.Street, Orders.HouseNumber from Orders inner join Users on Orders.IdClient=Users.Id
                            inner join OrderProducts on Orders.Id=OrderProducts.IdOrder
                            inner join Employees on Employees.Id=Orders.IdEmployee inner join Products on OrderProducts.IdProduct=Products.Id
                            inner join TypeOfProducts 
                            on TypeOfProducts.Id=Products.ProductTypeCode
                            where Orders.Id={id}";

			Read(query, order, GetOrder);

			return order;
		}
		private void GetOrder(SqlDataReader reader,Order order)
		{
			order.Id = reader.GetInt32(0);
			var client=new Client( reader.GetString(2), reader.GetString(3), reader.GetString(4));
			order.Client = client;
			order.OrderDate = reader.GetDateTime(5);
			order.DateOfSqipment=reader.GetDateTime(6);
			order.Status=reader.GetString(7);
			order.Orders = GetItems(reader.GetInt32(0));
			order.Price = reader.GetDecimal(8);
			var employee = new Employee(reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13),
				reader.GetString(14), reader.GetInt32(15));
			order.Employee = employee;	
			order.Street=reader.GetString(16);
			order.HouseNumber = reader.GetString(17);
		}
		private List<OrderItem> GetItems(int idOrder)
		{
			List<OrderItem> items= new List<OrderItem>();

			string query=$@"select Orders.Id,Products.Id, Products.ProductName, Products.ProductTypeCode, Products.Price,
                            OrderProducts.QuantityProducts 
							from OrderProducts 
							inner join Products
                            on OrderProducts.IdProduct=Products.Id inner join TypeOfProducts on 
                            TypeOfProducts.Id=Products.ProductTypeCode inner join Orders on Orders.Id=OrderProducts.IdOrder
                            where Orders.Id={idOrder}";

			Read(query, items, ProductsItems);

			return items;	
		}
		private void ProductsItems(SqlDataReader reader, List<OrderItem> items)
		{
			var product = new Product( reader.GetInt32(1),reader.GetString(2), reader.GetInt32(3), reader.GetDecimal(4));
			var item = new OrderItem(product, reader.GetInt32(5));
			items.Add(item);
		}


		public bool Update(Order item)
		{
			try
			{
				string query = _updateQuery;
				var connect = Connect();
				connect.Open();
				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.Parameters.Add("@idClient", SqlDbType.Int).Value = item.Client.Id;
				command.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = item.OrderDate;
				command.Parameters.Add("@dateOfSgipment", SqlDbType.DateTime).Value = item.DateOfSqipment;
				command.Parameters.Add("@status", SqlDbType.VarChar).Value = item.Status;
				command.Parameters.Add("@price", SqlDbType.Decimal).Value=item.Price;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = item.Employee.Id;
				command.Parameters.Add("@street", SqlDbType.VarChar).Value = item.Street;
				command.Parameters.Add("@houseNumber", SqlDbType.VarChar).Value = item.HouseNumber;
				command.ExecuteNonQuery();
				connect.Close();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<OrderItem> GetOrderItems(int idOrder)
		{
			List<OrderItem> list = new List<OrderItem>();

			Read(@$"select Orders.Id,Products.ProductName, Product.ProductTypeCode, Products.Price, OrderProducts.QuantityProducts
                    from Orders inner join OrderProducts on Orders.Id=OrderProducts.IdOrder inner join Products on OrderProducts.IdProduct=Products.Id
                    inner join TypeOfProducts on TypeOfProducts.Id=Products.ProductTypeCode where Orders.Id={idOrder}", list, GetItems);

			return list;
		}

		private void GetItems(SqlDataReader reader, List<OrderItem> list)
		{

			var product = new Product(reader.GetString(1), reader.GetInt32(2), reader.GetDecimal(3));
			var count=reader.GetInt32(4);

			var item=new OrderItem(product, count);

			list.Add(item);
		}
	}
}
