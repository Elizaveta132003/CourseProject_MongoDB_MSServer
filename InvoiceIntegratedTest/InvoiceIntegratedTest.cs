using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Infrastructure.Data.SQLServer;
using IntegratedTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace InvoiceIntegratedTest
{
	public class InvoiceIntegratedTest
	{
		private string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Kursovaya;Trusted_Connection=True;";

		[Fact, TestPriority(1)]
		public void TestCreateData()
		{
			try
			{
				//Arrange

				var userRepository = new UsersRepository(_connectionString);
				var employeeRepository = new EmployeesRepository(_connectionString);
				var orderRepository = new OrdersRepository(_connectionString);
				var invoiceRepository = new InvoiceRepository(_connectionString);
				var productsRepository = new ProductsRepository(_connectionString);

				List<Client> collectionUsers = GetTestUsers();

				Employee employee = GetTestEmployee();

				Product product = GetProduct();
				List<OrderItem> orderItems = GetTestOrderItem();
				Order order = GetTestOrder();
				Invoice invoice = GetTestInvoice();

				//Act

				employeeRepository.Create(employee);
				var collectionEmployee = employeeRepository.GetAll();
				Employee employeeData = collectionEmployee[0];

				productsRepository.Create(product);
				var collectionProducts = productsRepository.GetAll();
				Product productData = collectionProducts[0];

				collectionUsers.ForEach(user => userRepository.Create(user));
				List<Client> userData = userRepository.GetAll();
				order.Client = userData[0];
				order.Employee = employeeData;
				orderItems.ForEach(x => x.Product = productData);
				orderItems.ForEach(x => x.Price = x.Product.Price * x.Count);
				order.Orders = orderItems;
				order.Price = orderItems.Sum(orderItem => orderItem.Price);
				orderRepository.Create(order);
				var collectionOrders = orderRepository.GetAll();
				var orderData = collectionOrders[0];


				invoice.NameOrganization = userData[0].NameOrganization;
				invoice.Order = orderData;
				invoice.Client = userData[0];
				invoice.Employee = employeeData;

				invoiceRepository.Create(invoice);
				var collectionInvoice = invoiceRepository.GetAll();
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
		public void TestDeleteAllData()
		{
			try
			{
				//Arrange

				var userRepository = new UsersRepository(_connectionString);
				var employeeRepository = new EmployeesRepository(_connectionString);
				var orderRepository = new OrdersRepository(_connectionString);
				var invoiceRepository = new InvoiceRepository(_connectionString);
				var productsRepository = new ProductsRepository(_connectionString);

				//Act

				var collectionOrders = orderRepository.GetAll();
				Order order = collectionOrders[0];
				orderRepository.Delete(order);

				var collectionProduct = productsRepository.GetAll();
				Product product = collectionProduct[0];
				productsRepository.Delete(product);

				var collectionUser = userRepository.GetAll();
				collectionUser.ForEach(user => userRepository.Delete(user));

				//Assert

				Assert.True(true);
			}
			catch (Exception)
			{
				Assert.True(false);
			}
		}



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