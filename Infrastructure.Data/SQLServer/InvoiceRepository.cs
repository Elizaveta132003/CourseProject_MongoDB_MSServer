using Domain.Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Core.Models.Orders;
using System.Xml.Linq;

namespace Infrastructure.Data.SQLServer
{
	public class InvoiceRepository : ReadByDataBase,IRepository<Invoice>
	{

		private string _insertQuery= @$"INSERT INTO Invoice (RegistrationDate, NameOrganization, Street, HouseNumber, IdOrder, IdClient, IdEmployee) 
										VALUES(@registrationDate,@nameOrganization,@street, @houseNumber, @idOrder, @idClient,@idEmployee)";
		private string _getAllQuery = @"select Invoice.Id, Invoice.RegistrationDate, Invoice.NameOrganization, Invoice.Street,Invoice.HouseNumber, Invoice.IdClient,
                                        Invoice.IdEmployee, Invoice.IdOrder
                                        from Invoice";
			//@"select Invoice.Id, Invoice.RegistrationDate, Invoice.NameOrganization, Invoice.Street, 
			//						  Invoice.HouseNumber, Orders.Id, Orders.IdClient, Orders.OrderDate, Orders.DateOfSgipment,
			//						  Orders.Status, Orders.Price, Orders.IdEmployee, Orders.Street, Orders.HouseNumber, Users.Id, 
			//						  Users.NameOrganization, Users.PhoneNumber, Users.Password, Employees.Id, Employees.PhoneNumber,
			//						  Employees.Password, Employees.LastName, Employees.FirstName, Employees.MiddleName, Employees.PositionCode,
			//						  Employees.Hide
			//                                   from Invoice 
			//                                   inner join Orders
			//                                   on Invoice.IdOrder=Orders.Id
			//						  inner join Users 
			//                                   on Users.Id=Invoice.IdClient 
			//                                   inner join Employees
			//						  on Employees.Id=Invoice.IdEmployee";
		private string _updateQuery= @$"UPDATE Invoice SET RegistrationDate=@registrationDate, NameOrganization=@nameOrganization, Street=@street, 
										HouseNumber=@houseNumber, IdOrder=@idOrder, IdClient=@idClient, IdEmployee=@idEmployee where Id=@id";

		private IRepository<Order> _orderRepository;
		private IRepository<Client> _clientRepository;
		private IRepository<Employee> _employeeRepository;

		public InvoiceRepository(string connectionString, IRepository<Order> orderRepository, IRepository<Client> clientRepository, IRepository<Employee> employeeRepository) : base(connectionString)
		{
			_orderRepository = orderRepository;
			_clientRepository = clientRepository;
			_employeeRepository = employeeRepository;
		}

		public InvoiceRepository(string connectionString)
			: this(connectionString,
				  new OrdersRepository(connectionString),
				  new UsersRepository(connectionString),
				  new EmployeesRepository(connectionString))
		{
		}

		public bool Create(Invoice item)
		{
			try
			{
				var registrationDate = item.RegistrationDate;
				var nameOrganization = item.NameOrganization;
				var street = item.Street;
				var houseNumber = item.HouseNumber;
				var idOrder = item.Order.Id;
				var idClient = item.Client.Id;

				var connect = Connect();
				connect.Open();
				string query = _insertQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@registrationDate", SqlDbType.DateTime).Value = registrationDate;
				command.Parameters.Add("@nameOrganization", SqlDbType.VarChar).Value = nameOrganization;
				command.Parameters.Add("@street", SqlDbType.VarChar).Value = street;
				command.Parameters.Add("@houseNumber", SqlDbType.VarChar).Value = houseNumber;
				command.Parameters.Add("@idOrder", SqlDbType.Int).Value = idOrder;
				command.Parameters.Add("@idClient", SqlDbType.Int).Value = idClient;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = item.Employee.Id;

				int number = command.ExecuteNonQuery();
				connect.Close();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(Invoice item)
		{
			throw new NotImplementedException();
		}

		public List<Invoice> GetAll()
		{
			try
			{
				List<Invoice> invoices = new List<Invoice>();
				Read(_getAllQuery, invoices, GetAllInvoices);

				return invoices;
				
			}
			catch
			{
				throw new Exception("Not found");
			}
		}

		private void GetAllInvoices(SqlDataReader reader, List<Invoice> invoices)
		{
			var id=reader.GetInt32(0);
			var registrationDate = reader.GetDateTime(1);
			var nameOrganization = reader.GetString(2);
			var street = reader.GetString(3);
			var houseNumber = reader.GetString(4);
			var client = GetClient(reader);
			var employee = GetEmployee(reader);
			var order = GetOrder(reader);


			var invoice = new Invoice(id, registrationDate, nameOrganization, street, houseNumber, order, client, employee);

			invoices.Add(invoice);
		}

		private Client GetClient(SqlDataReader reader)
			=> _clientRepository.GetT(reader.GetInt32(5));

		//{
		//	return new Client( reader.GetString(15), reader.GetString(16), reader.GetString(17));
		//}
		private Employee GetEmployee(SqlDataReader reader) 
			=> _employeeRepository.GetT(reader.GetInt32(6));
		//{
		//	return new Employee(reader.GetInt32(18), reader.GetString(19), reader.GetString(20), reader.GetString(21), reader.GetString(22),
		//		reader.GetString(23), reader.GetInt32(24));
		//}
		private Order GetOrder(SqlDataReader reader)
			=> _orderRepository.GetT(reader.GetInt32(7));
		//{
		//	return new Order(reader.GetInt32(5), GetClient(reader), reader.GetDateTime(7), reader.GetDateTime(8),
		//		reader.GetString(9), GetItemProducts(reader.GetInt32(5)), reader.GetDecimal(10), GetEmployee(reader), reader.GetString(12), reader.GetString(13));
		//}
		

		private List<OrderItem> GetItemProducts(int idOrder)
		{
			List<OrderItem> orderItems = new List<OrderItem>();

			Read($@"select Products.Id, Products.ProductName, Product.ProductTypeCode, Products.Price, OrderProducts.QuantityProducts 
								   from OrderProducts 
								   inner join Products on OrderProducts.IdProduct=Products.Id 
								   inner join TypeOfProducts on Products.ProductTypeCode=TypeOfProducts.Id 
								   where OrderProducts.IdOrder={idOrder}", orderItems, ItemsInList);
			return orderItems;
		}
		private void ItemsInList(SqlDataReader reader, List<OrderItem> orderItems)
		{
			OrderItem item = new OrderItem(new Product( reader.GetString(1), reader.GetInt32(2), reader.GetDecimal(3)), reader.GetInt32(4));
			orderItems.Add(item);
		}

		public Invoice GetT(int id)
		{
			try
			{
				var invoice=new Invoice();
				Read(_getAllQuery, invoice, GetInvoice);

				return invoice;
			}
			catch
			{
				throw new Exception("Not found");
			}
		}

		private void GetInvoice(SqlDataReader reader, Invoice invoice)
		{
			invoice.Id = reader.GetInt32(0);
			invoice.RegistrationDate = reader.GetDateTime(1);
			invoice.NameOrganization = reader.GetString(2);
			invoice.Street = reader.GetString(3);
			invoice.HouseNumber = reader.GetString(4);
			invoice.Client = GetClient(reader);
			invoice.Employee = GetEmployee(reader);
			invoice.Order = GetOrder(reader);
		}

		public bool Update(Invoice item)
		{
			try
			{
				var id=item.Id;
				var registrationDate=item.RegistrationDate;
				var nameOrganization=item.NameOrganization;
				var street=item.Street;
				var houseNumber=item.HouseNumber;
				var idOrder = item.Order.Id;
				var idClient = item.Client.Id;
				var idEmployee = item.Employee.Id;

				var connect = Connect();
				connect.Open();
				string query = _updateQuery;

				SqlCommand command = new SqlCommand(query, connect);
				command.Parameters.Add("@id", SqlDbType.Int).Value = item.Id;
				command.Parameters.Add("@registrationDate", SqlDbType.DateTime).Value = registrationDate;
				command.Parameters.Add("@nameOrganization", SqlDbType.VarChar).Value = nameOrganization;
				command.Parameters.Add("@street", SqlDbType.VarChar).Value = street;
				command.Parameters.Add("@houseNumber", SqlDbType.VarChar).Value = houseNumber;
				command.Parameters.Add("@idOrder", SqlDbType.Int).Value = idOrder;
				command.Parameters.Add("@idClient", SqlDbType.Int).Value = idClient;
				command.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
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
