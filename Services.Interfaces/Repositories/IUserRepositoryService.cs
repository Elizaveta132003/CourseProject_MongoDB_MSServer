using Domain.Core.Models;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
	public interface IUserRepositoryService
	{
		BaseResponse<bool> CreateUser(Client client);
		BaseResponse<bool> DeleteUser(int id);
		BaseResponse<bool> UpdateUser(Client client);
		BaseResponse<List<Client>> GetAllUsers();
		BaseResponse<Client> GetUserById(int id);
	}
}
