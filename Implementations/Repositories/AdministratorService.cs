using Domain.Core.Models;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
	public class AdministratorService : IAdministratorService
	{
		public IRepository<Employee> EmployeeRepository { get; set; }
		public IRepository<Client> ClientRepository { get; set; }


		public AdministratorService(IRepository<Employee> employeeRepository, IRepository<Client> clientRepository)
		{
			EmployeeRepository = employeeRepository;
			ClientRepository = clientRepository;
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

		public BaseResponse<List<Employee>> GetAllEmployees()
		{
			try
			{
				var data = EmployeeRepository.GetAll();
				return new BaseResponse<List<Employee>>()
				{
					Data = data,
					Description = "All employees get secessfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Employee>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> DeleteEmployee(Employee employee)
		{
			try
			{
				if (employee == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Null object",
						StatusCode = new NotFoundResult()
					};
				}

				EmployeeRepository.Delete(employee);

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

		public BaseResponse<bool> CreateEmployee(Employee employee)
		{
			try
			{
				if (employee == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Employee is null",
						StatusCode = new NotFoundResult()
					};
				}

				EmployeeRepository.Create(employee);

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

		public BaseResponse<bool> Update(Employee employee)
		{
			try
			{
				var value = EmployeeRepository.GetT(employee.Id);

				if (value == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found employee",
						StatusCode = new NotFoundResult()
					};
				}

				EmployeeRepository.Update(employee);

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
