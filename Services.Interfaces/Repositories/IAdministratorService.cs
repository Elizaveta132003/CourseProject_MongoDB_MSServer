using Domain.Core.Models;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
    public interface IAdministratorService
    {
        BaseResponse<List<Client>> GetAllUsers();
        BaseResponse<List<Employee>> GetAllEmployees();
        BaseResponse<bool> DeleteEmployee(Employee employee);
        BaseResponse<bool> CreateEmployee(Employee employee);
        BaseResponse<bool> CreateUser(Client client);
        BaseResponse<bool> Update(Employee employee);

    }
}
