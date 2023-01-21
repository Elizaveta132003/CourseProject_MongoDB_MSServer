using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
	public class OrderRepositoryService : IOrderRepositoryService
	{
		public IRepository<Order> OrderRepository { get; set; }

		public OrderRepositoryService(IRepository<Order> repository)
		{
			OrderRepository = repository;
		}

		public BaseResponse<bool> CreateOrder(Order order)
		{
			try
			{
				var data = OrderRepository.Create(order);

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

		public BaseResponse<bool> DeleteOrder(int id)
		{
			try
			{
				if (id == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				OrderRepository.Delete(id);

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

		public BaseResponse<List<Order>> GetAllOrder()
		{
			try
			{
				var data = OrderRepository.GetAll();

				if (data.Count == 0)
				{
					return new BaseResponse<List<Order>>()
					{
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				return new BaseResponse<List<Order>>()
				{
					Data = data,
					Description = "Completed secessfully",
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

		public BaseResponse<bool> UpdateOrder(Order order)
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

		public BaseResponse<Order> GetOrder(int id)
		{
			try
			{
				var data = OrderRepository.GetT(id);

				if (data == null)
				{
					return new BaseResponse<Order>()
					{
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				return new BaseResponse<Order>()
				{
					Data = data,
					Description = "Completed secessfully",
					StatusCode = new OkResult()
				};

			}
			catch (Exception ex)
			{
				return new BaseResponse<Order>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

	}
}
