using Domain.Core.Models.Roles;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class EmployeesRepository : IRepository<Employee>
	{
		public bool Create(Employee item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Employee item)
		{
			throw new NotImplementedException();
		}

		public List<Employee> GetAll()
		{
			throw new NotImplementedException();
		}

		public Employee GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Employee item)
		{
			throw new NotImplementedException();
		}
	}
}
