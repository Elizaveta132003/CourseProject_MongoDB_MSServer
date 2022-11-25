using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
    internal class CommodityExpertService : ICommodityExpertService
	{
		public IRepository<Order> OrderRepository { get; set; }
		public IRepository<Invoice> InvoiceRepository { get; set; }

		public CommodityExpertService(IRepository<Order> orderRepository, IRepository<Invoice> invoiceRepository)
		{
			OrderRepository = orderRepository;
			InvoiceRepository = invoiceRepository;
		}

		public BaseResponse<bool> CreateInvoice(Invoice invoice)
		{
			try
			{
				if (invoice == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				var data = InvoiceRepository.Create(invoice);

				return new BaseResponse<bool>()
				{
					Data = data,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = true,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<List<Order>> GetAllOrders()
		{
			try
			{
				var data = OrderRepository.GetAll();

				return new BaseResponse<List<Order>>()
				{
					Data = data,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Order>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> UpdateOrders(Order order)
		{
			try
			{
				var value = OrderRepository.GetT(order.Id);

				if (value == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				order.Status = "Delivered";
				OrderRepository.Update(order);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}
	}
}
