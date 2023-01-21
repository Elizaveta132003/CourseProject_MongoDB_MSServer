using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FunctionEntities.Services;
using Services.Interfaces.Functions;

namespace Services.Implementations.FunctionEntities.Services
{
	public class ReportService : IReportService
	{
		private IReportFuction _reportFuction;

		public ReportService(IReportFuction reportFuction)
		{
			_reportFuction = reportFuction;
		}

		public BaseResponse<Dictionary<int, int>> NumberProductsSold(List<Order> orders)
		{
			try
			{
				if (orders == null)
				{
					return new BaseResponse<Dictionary<int, int>>()
					{
						StatusCode = new NotFoundResult(),
						Description = "Not found"
					};
				}

				var productSold = _reportFuction.NumberProductsSold(orders);

				return new BaseResponse<Dictionary<int, int>>()
				{
					Data = productSold,
					StatusCode = new OkResult(),
					Description = "Completed seccesfully"
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Dictionary<int, int>>()
				{
					StatusCode = new NotFoundResult(),
					Description = ex.Message
				};
			}
		}

		public BaseResponse<Dictionary<Product, int>> ProductsInStock(List<Stock> stocks)
		{
			try
			{
				if (stocks == null)
				{
					return new BaseResponse<Dictionary<Product, int>>()
					{
						StatusCode = new NotFoundResult(),
						Description = "Not found"
					};
				}

				var productsInStock = _reportFuction.ProductsInStock(stocks);

				return new BaseResponse<Dictionary<Product, int>>()
				{
					Data = productsInStock,
					StatusCode = new OkResult(),
					Description = "Completed seccesfully"
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Dictionary<Product, int>>()
				{
					StatusCode = new NotFoundResult(),
					Description = ex.Message
				};
			}
		}
	}
}
