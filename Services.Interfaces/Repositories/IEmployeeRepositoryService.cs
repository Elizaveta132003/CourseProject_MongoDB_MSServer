using Domain.Core.Models;
using Domain.Core.Models.Roles;

namespace Services.Interfaces.Repositories
{
	public interface IEmployeeRepositoryService
	{
		BaseResponse<bool> CreateEmployee(Employee employee);
		BaseResponse<bool> UpdateEmployee(Employee employee);
		BaseResponse<bool> DeleteEmployee(int id);
		BaseResponse<List<Employee>> GetAllEmployee();
		BaseResponse<Employee> GetEmployeeById(int id);
	}
}
