using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using Services.Interfaces.Functions;
using Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Functions
{
	public class UserFunction : IUserFunction
	{
		public Client ViewInformationAboutUser(Client client)
			=> client;

		public List<Order> ViewUserOrders(Client client, List<Order> orders)
		{
			List<Order> ordersUser = new List<Order>();

			foreach (var order in orders)
			{
				if (order.Client.Id == client.Id)
				{
					ordersUser.Add(order);
				}
			}

			return ordersUser;
		}

	}
}
