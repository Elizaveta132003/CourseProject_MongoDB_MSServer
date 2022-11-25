using Domain.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IOrdersRepository
	{
		public List<OrderItem> GetOrderItems(int idOrder);
	}
}
