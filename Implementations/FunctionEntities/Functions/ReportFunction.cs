using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Services.Interfaces.Functions;

namespace Services.Implementations.Functions
{
	public class ReportFunction : IReportFuction
	{
		public Dictionary<Product, int> ProductsInStock(List<Stock> stocks)
		{
			Dictionary<Product, int> result = new Dictionary<Product, int>();

			var g = stocks.GroupBy(s => s.Product).ToDictionary(k => k.Key, k => k.ToList());

			foreach (var stock in g)
			{
				var productt = stock.Key;
				var list = stock.Value;
				int sum = 0;

				foreach (var product in list)
				{
					sum += product.Count;


				}
				result.Add(productt, sum);

			}

			return result;
		}
		public Dictionary<int, int> NumberProductsSold(List<Order> orders)
		{
			Dictionary<int, int> result = new Dictionary<int, int>();
			int type = 0;
			int sum = 0;

			foreach (Order order in orders)
			{
				order.Orders.ForEach(x => type = x.Product.Type);
				sum = order.Orders.Sum(x => x.Count);
				result.Add(type, sum);
			}

			return result;
		}
	}
}
