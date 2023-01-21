using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FunctionEntities.Services;
using Services.Interfaces.Functions;

namespace Services.Implementations.FunctionEntities.Services
{
	public class UserService : IUserService
	{
		private IUserFunction _userFunction;

		public UserService(IUserFunction userFunction)
		{
			_userFunction = userFunction;
		}

		public BaseResponse<Client> ViewInformationAboutUser(Client client)
		{
			try
			{
				if (client == null)
				{
					return new BaseResponse<Client>()
					{
						StatusCode = new NotFoundResult(),
						Description = "Not found"
					};
				}

				var informatioClient = _userFunction.ViewInformationAboutUser(client);

				return new BaseResponse<Client>()
				{
					Data = informatioClient,
					StatusCode = new OkResult(),
					Description = "Completed seccesfully"
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Client>()
				{
					StatusCode = new NotFoundResult(),
					Description = ex.Message
				};
			}
		}

		public BaseResponse<List<Order>> ViewUserOrders(Client client, List<Order> order)
		{
			try
			{
				if (client == null)
				{
					return new BaseResponse<List<Order>>()
					{
						StatusCode = new NotFoundResult(),
						Description = "Not found"
					};
				}

				var userOrders = _userFunction.ViewUserOrders(client, order);

				return new BaseResponse<List<Order>>()
				{
					Data = userOrders,
					StatusCode = new OkResult(),
					Description = "Completed seccesfully"
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Order>>()
				{
					StatusCode = new NotFoundResult(),
					Description = ex.Message
				};
			}
		}
	}
}
