using Domain.Core.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class StockRepository : IRepository<Stock>
	{
		public bool Create(Stock item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Stock item)
		{
			throw new NotImplementedException();
		}

		public List<Stock> GetAll()
		{
			throw new NotImplementedException();
		}

		public Stock GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Stock item)
		{
			throw new NotImplementedException();
		}
	}
}
