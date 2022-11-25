using Domain.Core.Models.Orders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class OrdersRepository : IRepository<Order>, IOrdersRepository
	{
		public bool Create(Order item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Order item)
		{
			throw new NotImplementedException();
		}

		public List<Order> GetAll()
		{
			throw new NotImplementedException();
		}

		public List<OrderItem> GetOrderItems(int idOrder)
		{
			throw new NotImplementedException();
		}

		public Order GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Order item)
		{
			throw new NotImplementedException();
		}
	}
}
