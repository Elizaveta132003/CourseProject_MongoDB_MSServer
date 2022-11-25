using Domain.Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class InvoiceRepository : IRepository<Invoice>
	{
		public bool Create(Invoice item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Invoice item)
		{
			throw new NotImplementedException();
		}

		public List<Invoice> GetAll()
		{
			throw new NotImplementedException();
		}

		public Invoice GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Invoice item)
		{
			throw new NotImplementedException();
		}
	}
}
