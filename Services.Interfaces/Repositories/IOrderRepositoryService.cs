using Domain.Core.Models;
using Domain.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
	public interface IOrderRepositoryService
	{
		BaseResponse<bool> CreateOrder(Order order);
		BaseResponse<bool> UpdateOrder(Order order);
		BaseResponse<bool> DeleteOrder(int id);
		BaseResponse<List<Order>> GetAllOrder();
		BaseResponse<Order> GetOrder(int id);
	}
}
