using Domain.Core.Models.Roles;

namespace Domain.Core.Models.Orders
{
	public class Order : EntityBase
	{
		public Client Client { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DateOfSqipment { get; set; }
		public string Status { get; set; }
		public List<OrderItem> Orders { get; set; }
		public decimal Price { get; set; }
		public Employee Employee { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }

		public Order(int id, Client client, DateTime orderDate, DateTime dateOfSqipment, string status, List<OrderItem> orders, decimal price, Employee employee, string street, string houseNumber)
		{
			Id = id;
			Client = client;
			OrderDate = orderDate;
			DateOfSqipment = dateOfSqipment;
			Status = status;
			Orders = orders;
			Price = GetPrice(orders);
			Employee = employee;
			Street = street;
			HouseNumber = houseNumber;
		}
		public Order(Client client, DateTime orderDate, DateTime dateOfSqipment, string status, List<OrderItem> orders, decimal price, Employee employee, string street, string houseNumber)
		{
			Client = client;
			OrderDate = orderDate;
			DateOfSqipment = dateOfSqipment;
			Status = status;
			Orders = orders;
			Price = GetPrice(orders);
			Employee = employee;
			Street = street;
			HouseNumber = houseNumber;
		}

		private decimal GetPrice(List<OrderItem> orders)
		{
			decimal price = 0;

			foreach (var item in orders)
			{
				price += item.Price;
			}

			return price;
		}
		public Order(List<OrderItem> orders)
		{
			Price = GetPrice(orders);
		}
		public Order() { }

		public override string? ToString()
			=> $"{Id}, {Client.Id}, {OrderDate}, {DateOfSqipment}, {Status}, {Price}, {Employee.Id}, {Street}, {HouseNumber}";

		/*public Order(int id, Client client, DateTime orderDate, DateTime dateOfSqipment, string status, List<OrderItem> orders, decimal price, Employee employee)
		{
			Id = id;
			Client = client;
			OrderDate = orderDate;
			DateOfSqipment = dateOfSqipment;
			Status = status;
			Orders = orders;
			Price = price;
			Employee = employee;
		}*/




	}
}
