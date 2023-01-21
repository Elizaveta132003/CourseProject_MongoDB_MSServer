using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;

namespace Services.Interfaces.Functions
{
	public interface IReportFuction
	{
		public Dictionary<Product, int> ProductsInStock(List<Stock> stocks);
		public Dictionary<int, int> NumberProductsSold(List<Order> orders);
	}
}
