using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Functions
{
	public interface IUserFunction
	{
		public List<Order> ViewUserOrders(Client client, List<Order> orders);
		public Client ViewInformationAboutUser(Client client);
	}
}
