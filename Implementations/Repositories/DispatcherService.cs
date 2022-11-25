using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Repositories
{
	internal class DispatcherService : IDispatcherService
	{
		IRepository<Order> OrderRepository { get; set; }

		public DispatcherService(IRepository<Order> orderRepository)
		{
			OrderRepository = orderRepository;
		}
		public BaseResponse<bool> CreatePlanOfProduce(Order order)
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

				order.Status = "In stock";

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

		public BaseResponse<bool> Update(Order order)
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
