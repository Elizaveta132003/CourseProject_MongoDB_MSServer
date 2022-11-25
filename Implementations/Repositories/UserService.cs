using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
	public class UserService : IUserService
	{
		public IRepository<Product> ProductRepository { get; set; }
		public IRepository<Client> ClientRepository { get; set; }
		public IRepository<Order> OrderRepository { get; set; }
		//public Client Client { get; set; }

		public UserService(IRepository<Product> productRepository, IRepository<Client> clientRepository, IRepository<Order> orderRepository)
		{
			ProductRepository = productRepository;
			ClientRepository = clientRepository;
			OrderRepository = orderRepository;

		}

		public BaseResponse<List<Product>> GetAllProduct()
		{
			try
			{
				var data = ProductRepository.GetAll();

				return new BaseResponse<List<Product>>()
				{
					Data = data,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};

			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Product>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		/*public BaseResponse<List<Order>> GetAllUserOrder(Client client)
        {
            try
            {
                if (client == null)
                {
                    return new BaseResponse<List<Order>>()
                    {
                        Description = "Not found",
                        StatusCode = new NotFoundResult()
                    };
                }

                var data=OrderRepository.GetT()
            }
            catch(Exception ex)
            {

            }
        }*/

		public BaseResponse<Client> InformationAboutClient(Client client)
		{
			try
			{
				if (client == null)
				{
					return new BaseResponse<Client>()
					{
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				return new BaseResponse<Client>()
				{
					Data = client,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Client>()
				{
					Description = "Not found",
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> PlaceAnOrder(Order order)
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

		public BaseResponse<bool> UpdateInformationAboutClient(Client client)
		{
			try
			{
				var value = ClientRepository.GetT(client.Id);

				if (value == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				var data = ClientRepository.Update(client);

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
