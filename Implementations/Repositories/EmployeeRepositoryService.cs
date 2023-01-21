using Domain.Core.Models;
using Domain.Core.Models.Roles;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
	public class EmployeeRepositoryService : IEmployeeRepositoryService
	{
		public IRepository<Employee> EmployeeRepository { get; set; }

		public EmployeeRepositoryService(IRepository<Employee> employeeRepository)
		{
			EmployeeRepository = employeeRepository;
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

		public BaseResponse<bool> DeleteEmployee(int id)
		{
			try
			{
				if (id == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Null object",
						StatusCode = new NotFoundResult()
					};
				}

				EmployeeRepository.Delete(id);

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

		public BaseResponse<List<Employee>> GetAllEmployee()
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

		public BaseResponse<Employee> GetEmployeeById(int id)
		{
			try
			{
				var data = EmployeeRepository.GetT(id);

				if (data == null)
				{
					return new BaseResponse<Employee>()
					{
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				return new BaseResponse<Employee>()
				{
					Data = data,
					Description = "All employees get secessfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Employee>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> UpdateEmployee(Employee employee)
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
