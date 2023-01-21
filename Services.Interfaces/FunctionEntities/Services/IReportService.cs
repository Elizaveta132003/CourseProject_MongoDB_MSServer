using Domain.Core.Models.Products;
using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Models.Orders;

namespace Services.Interfaces.FunctionEntities.Services
{
	public interface IReportService
	{
		public BaseResponse<Dictionary<Product, int>> ProductsInStock(List<Stock> stocks);
		public BaseResponse<Dictionary<int, int>> NumberProductsSold(List<Order> orders);
	}
}
