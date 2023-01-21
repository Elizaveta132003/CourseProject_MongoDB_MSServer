using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Infrastructure.Data.SQLServer;
using Services.Implementations.Repositories;
using Services.Implementations.Functions;
using Services.Implementations.FunctionEntities.Services;

namespace IntegratedTest
{
	[TestCaseOrderer("IntegratedTest.Configuration", "IntegratedTest")]
	public class InvoiceIntegrationTest
	{
		private string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Kursovaya;Trusted_Connection=True;";
		//private string _connectionString = "mongodb://localhost:27017";

		[Fact, TestPriority(1)]
		public void TestCreateData()
		{
			try
			{
				//Arrange

				var userRepository = new UsersRepository(_connectionString);
				var userService = new UserRepositoryService(userRepository);

				var employeeRepository = new EmployeesRepository(_connectionString);
				var employeeService = new EmployeeRepositoryService(employeeRepository);

				var orderRepository = new OrdersRepository(_connectionString);
				var orderService = new OrderRepositoryService(orderRepository);

				var invoiceRepository = new InvoiceRepository(_connectionString);
				var invoiceService = new InvoiceRepositoryService(invoiceRepository);

				var productsRepository = new ProductsRepository(_connectionString);
				var productService = new ProductRepositoryService(productsRepository);

				List<Client> collectionUsers = GetTestUsers();

				Employee employee = GetTestEmployee();

				Product product = GetProduct();
				List<OrderItem> orderItems = GetTestOrderItem();
				Order order = GetTestOrder();
				Invoice invoice = GetTestInvoice();

				//Act

				employeeService.CreateEmployee(employee);
				var collectionEmployee = employeeService.GetAllEmployee().Data;
				Employee employeeData = collectionEmployee[0];

				productService.CreateProduct(product);
				var collectionProducts = productService.GetAllProducts().Data;
				Product productData = collectionProducts[0];

				collectionUsers.ForEach(user => userService.CreateUser(user));
				List<Client> userData = userService.GetAllUsers().Data;

				order.Client = userData[0];
				order.Employee = employeeData;
				orderItems.ForEach(x => x.Product = productData);
				orderItems.ForEach(x => x.Price = x.Product.Price * x.Count);
				order.Orders = orderItems;
				order.Price = orderItems.Sum(orderItem => orderItem.Price);
				orderService.CreateOrder(order);

				var collectionOrders = orderService.GetAllOrder().Data;
				var orderData = collectionOrders[0];


				invoice.NameOrganization = userData[0].NameOrganization;
				invoice.Order = orderData;
				invoice.Client = userData[0];
				invoice.Employee = employeeData;

				invoiceService.CreateInvoice(invoice);
				var collectionInvoice = invoiceService.GetAllInvoice().Data;
				Invoice invoice1 = collectionInvoice[0];

				//Assert

				Assert.True(true);
			}
			catch (Exception ex)
			{
				Assert.True(false);
			}
		}

		[Fact, TestPriority(2)]
		public void TestProductInStock()
		{
			try
			{
				//Arrange
				var collectionStock = GetStocks();
				var report = new ReportFunction();
				var reportService = new ReportService(report);

				//Act
				var result = reportService.ProductsInStock(collectionStock).Data;

				//Assert
				Assert.True(true);

			}
			catch (Exception ex)
			{
				Assert.True(false);
			}
		}

		[Fact, TestPriority(2)]
		public void TestNumberProductsSold()
		{
			try
			{
				//Arrange
				var orders = GetListOrders();
				var report = new ReportFunction();
				var reportService = new ReportService(report);

				//Act
				var result = reportService.NumberProductsSold(orders).Data;

				//Assert

				Assert.True(true);
			}
			catch (Exception ex)
			{
				Assert.True(false);
			}
		}


		[Fact, TestPriority(2)]
		public void TestDeleteAllData()
		{
			try
			{
				//Arrange

				var userRepository = new UsersRepository(_connectionString);
				var userService = new UserRepositoryService(userRepository);

				var employeeRepository = new EmployeesRepository(_connectionString);
				var employeeService = new EmployeeRepositoryService(employeeRepository);

				var orderRepository = new OrdersRepository(_connectionString);
				var orderService = new OrderRepositoryService(orderRepository);

				var invoiceRepository = new InvoiceRepository(_connectionString);
				var invoiceService = new InvoiceRepositoryService(invoiceRepository);

				var productsRepository = new ProductsRepository(_connectionString);
				var productService = new ProductRepositoryService(productsRepository);

				//Act

				var collectionOrders = orderService.GetAllOrder().Data;
				Order order = collectionOrders[0];
				orderService.DeleteOrder(order.Id);

				var collectionProduct = productService.GetAllProducts().Data;
				Product product = collectionProduct[0];
				productService.DeleteProduct(product.Id);

				var collectionUser = userService.GetAllUsers().Data;
				collectionUser.ForEach(user => userService.DeleteUser(user.Id));

				//Assert

				Assert.True(true);
			}
			catch (Exception ex)
			{
				Assert.True(false);
			}
		}
		private List<Stock> GetStocks()
			=> new List<Stock>()
			{
				new Stock()
				{
					DateOfReceipt=DateTime.Now,
					OrderDate=DateTime.Now,
					DateOfDispatch=DateTime.Now,
					Product=GetProduct(),
					Count=1

				},
				new Stock()
				{
					DateOfReceipt=DateTime.Now,
					OrderDate=DateTime.Now,
					DateOfDispatch=DateTime.Now,
					Product=GetProduct(),
					Count=1

				}

			};
		private List<Client> GetTestUsers()
			=> new List<Client>()
			{
				new Client()
				{
					NameOrganization="Client1",
					PhoneNumber="+375444444444",
					Password="1111111Adfv"
				},
				new Client()
				{
					NameOrganization="Client2",
					PhoneNumber="+375299999999",
					Password="2222222Hdbvc"
				},
				new Client()
				{
					NameOrganization="Client3",
					PhoneNumber="+375295555555",
					Password="3333333Pabcys"
				}
			};

		private Employee GetTestEmployee()
			=> new CommodityExpert()
			{
				PhoneNumber = "+375441111111",
				Password = "16254Dafsv",
				LastName = "Aaaaa",
				FirstName = "Bbbbbb",
				MiddleName = "Cccccc",
				PositionCode = 4
			};
		private Order GetTestOrder()
			 => new Order()
			 {
				 Client = GetTestUsers()[0],
				 Employee = GetTestEmployee(),
				 OrderDate = DateTime.Now,
				 DateOfSqipment = new DateTime(2022, 12, 31),
				 Street = "ygsfvd",
				 HouseNumber = "21",
				 Orders = null,
				 Status = "Delivered"
			 };
		private List<Order> GetListOrders()
			=> new List<Order>()
			{
				new Order()
				{
				Client = GetTestUsers()[0],
				 Employee = GetTestEmployee(),
				 OrderDate = DateTime.Now,
				 DateOfSqipment = new DateTime(2022, 12, 31),
				 Street = "ygsfvd",
				 HouseNumber = "21",
				 Orders =GetListOrderss(),
				 Status = "Delivered"
				}
			};
		private List<OrderItem> GetListOrderss()
			=> new List<OrderItem>()
			{
				new OrderItem()
				{
					Product=GetProduct(),
					Count=2
				}
			};

		private List<OrderItem> GetTestOrderItem()
			=> new List<OrderItem>()
			{
				new OrderItem()
				{
					Product = null,
					Count=2
				}
			};
		private Product GetProduct()
			=> new Product()
			{
				Name = "Rose",
				Type = 3,
				Price = 15
			};
		private Invoice GetTestInvoice()
			=> new Invoice()
			{
				RegistrationDate = DateTime.Now,
				Street = "streetOne",
				HouseNumber = "23"
			};
	}
}
