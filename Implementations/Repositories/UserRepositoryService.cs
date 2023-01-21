using Domain.Core.Models;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
	public class UserRepositoryService : IUserRepositoryService
	{
		public IRepository<Client> ClientRepository { get; set; }

		public UserRepositoryService(IRepository<Client> clientRepository)
		{
			ClientRepository = clientRepository;
		}

		public BaseResponse<bool> CreateUser(Client client)
		{
			try
			{
				if (client == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Client is null",
						StatusCode = new NotFoundResult()
					};
				}

				ClientRepository.Create(client);

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

		public BaseResponse<bool> DeleteUser(int id)
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

				ClientRepository.Delete(id);

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

		public BaseResponse<List<Client>> GetAllUsers()
		{
			try
			{
				var data = ClientRepository.GetAll();
				return new BaseResponse<List<Client>>()
				{
					Data = data,
					Description = "All users get secessfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Client>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<Client> GetUserById(int id)
		{
			try
			{
				var data = ClientRepository.GetT(id);

				if (data == null)
				{
					return new BaseResponse<Client>()
					{
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				return new BaseResponse<Client>()
				{
					Data = data,
					Description = "All users get secessfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Client>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> UpdateUser(Client client)
		{
			try
			{
				if (client == null)
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
