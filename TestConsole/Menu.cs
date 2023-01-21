using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Services.Implementations.Repositories;
using Services.Interfaces.Repositories;
using Services.Interfaces.Validations;

namespace TestConsole
{
	public class Menu
	{
		private IOrderRepositoryService _orderRepositoryService;

		public Menu()
		{
			_orderRepositoryService = new OrderRepositoryService(MenuOutput());
		}
		public IRepository<Order> MenuOutput()
		{
			var flag = true;
			while (flag)
			{
				MenuForChoiceDataBase();
				var choiceDataBase = Console.ReadLine();
				if (choiceDataBase == "1")
					return new Infrastructure.Data.MongoDB.OrdersRepository("mongodb://localhost:27017");
				else if (choiceDataBase == "2")
					return new Infrastructure.Data.SQLServer.OrdersRepository(@"Server=(localdb)\MSSQLLocalDB;Database=Kursovaya;Trusted_Connection=True;");
			}

			return null;
		}


		public void SwichCaseMenu()
		{
			UsualMenu();

			while (true)
			{
				var choiceDataBase = Console.ReadLine();

				switch (choiceDataBase)
				{
					case "1":
						_orderRepositoryService = new OrderRepositoryService(MenuOutput());
						SwichCaseMenu();
						break;
					case "2":
						OutputAll();
						SwichCaseMenu();
						break;
					case "3":
						GetById();
						SwichCaseMenu();
						break;
					case "4":

						InputOrder();
						SwichCaseMenu();
						break;
					case "5":
						Delete();
						SwichCaseMenu();
						break;
					default:
						Console.WriteLine("You entered something wrong");
						SwichCaseMenu();
						break;

				}

			}

		}
		private void Delete()
		{
			var flag = true;

			while (flag)
			{
				Console.WriteLine("Input Id:");
				var id = Console.ReadLine();


				if (Validation.ObjIsNumber(id) && Validation.NumberIsInt(Convert.ToInt32(id)) && Validation.NumberIsPositive(id))
				{
					var data = _orderRepositoryService.DeleteOrder(Convert.ToInt16(id));
					Console.WriteLine($"{data.Description}");
					flag = false;
				}
				else
				{
					Console.WriteLine("Number is not recognized. Enter again.");

				}
			}
		}

		private void GetById()
		{
			var flag = true;

			while (flag)
			{
				Console.WriteLine("Input Id:");
				var id = Console.ReadLine();


				if (Validation.ObjIsNumber(id) && Validation.NumberIsInt(Convert.ToInt32(id)) && Validation.NumberIsPositive(id))
				{
					var data = _orderRepositoryService.GetOrder(Convert.ToInt32(id));
					OutputData<Order>.OutputObj(data.Data);
					Console.WriteLine($"{data.Description}");
					flag = false;
				}
				else
				{
					Console.WriteLine("Number is not recognized. Enter again.");

				}
			}
		}
		private void OutputAll()
		{
			var data = _orderRepositoryService.GetAllOrder();
			if (data.Data == null)
			{
				Console.WriteLine($"{data.Description}");
			}
			else
				OutputData<Order>.OutputList(data.Data);

		}
		private static void MenuForChoiceDataBase()
		{
			Console.WriteLine("Choose a database");
			Console.WriteLine("1-MongoDB");
			Console.WriteLine("2-SQL Server");
		}
		private void UsualMenu()
		{
			Console.WriteLine("1-Change the database type");
			Console.WriteLine("2-Get all orders");
			Console.WriteLine("3-Get an order by id");
			Console.WriteLine("4-Create a new order");
			Console.WriteLine("5-Delete an order");
		}
		private void DeleteOrder()
		{

		}
		private void InputOrder()
		{

			var orders = GetTestOrderItem();
			Console.WriteLine("Input date of sqipment: ");
			var date = Convert.ToDateTime(Console.ReadLine());


			Console.WriteLine("Input street: ");
			var street = Console.ReadLine();

			while (!IIsNotNull.IsNotNull(street))
			{
				Console.WriteLine("You entered something wrong");
				Console.WriteLine("Input street: ");
				street = Console.ReadLine();
			}

			Console.WriteLine("Input houseNumber: ");
			var houseNumber = Console.ReadLine();

			while (!IIsNotNull.IsNotNull(houseNumber))
			{
				Console.WriteLine("You entered something wrong");
				Console.WriteLine("Input houseNumber: ");
				houseNumber = Console.ReadLine();
			}

			var orderDate = DateTime.Now;
			orders.ForEach(x => x.Price = x.Product.Price * x.Count);
			var price = orders.Sum(orderItem => orderItem.Price);

			var order = new Order()
			{
				Client = Client(),
				OrderDate = orderDate,
				DateOfSqipment = date,
				Orders = orders,
				Price = price,
				Employee = GetTestEmployee(),
				Street = street,
				HouseNumber = houseNumber
			};

			var data = _orderRepositoryService.CreateOrder(order);
			Console.WriteLine($"{data.Description}");
		}

		private Client Client()
			=> new Client()
			{
				Id = 15053,
				NameOrganization = "Client1",
				PhoneNumber = "+375299999999",
				Password = "11111111"
			};
		private Employee GetTestEmployee()
			=> new CommodityExpert()
			{
				Id = 12037,
				PhoneNumber = "+375441111111",
				Password = "16254Dafsv",
				LastName = "Aaaaa",
				FirstName = "Bbbbbb",
				MiddleName = "Cccccc",
				PositionCode = 4
			};
		private List<OrderItem> GetTestOrderItem()
			=> new List<OrderItem>()
			{
				new OrderItem()
				{
					Product = GetProduct(),
					Count=2
				}
			};
		private Product GetProduct()
			=> new Product()
			{
				Id = 12027,
				Name = "Rose",
				Type = 3,
				Price = 15
			};
	}

}
