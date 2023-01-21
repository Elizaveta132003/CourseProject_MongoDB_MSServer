using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FunctionEntities.Services
{
	public interface IUserService
	{
		public BaseResponse<Client> ViewInformationAboutUser(Client client);
		public BaseResponse<List<Order>> ViewUserOrders(Client client, List<Order> order);
	}
}
