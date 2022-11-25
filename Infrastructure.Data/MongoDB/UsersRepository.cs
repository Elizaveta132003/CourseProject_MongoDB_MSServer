using Domain.Core.Models.Roles;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class UsersRepository : IRepository<Client>
	{
		public bool Create(Client item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Client item)
		{
			throw new NotImplementedException();
		}

		public List<Client> GetAll()
		{
			throw new NotImplementedException();
		}

		public Client GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Client item)
		{
			throw new NotImplementedException();
		}
	}
}
