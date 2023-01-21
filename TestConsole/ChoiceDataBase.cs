
using Domain.Core.Models.Orders;
using Domain.Interfaces;

namespace TestConsole
{
	public static class ChoiceDataBase
	{
		public static IRepository<Order> Con(string number)
		{
			if (number == "1")
				return new Infrastructure.Data.MongoDB.OrdersRepository("mongodb://localhost:27017");
			else if (number == "2")
				return new Infrastructure.Data.SQLServer.OrdersRepository(@"Server=(localdb)\MSSQLLocalDB;Database=Kursovaya;Trusted_Connection=True;");
			return null;
		}
	}
}
