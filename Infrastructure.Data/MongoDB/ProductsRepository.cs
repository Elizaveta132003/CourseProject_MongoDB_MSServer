using Domain.Core.Models.Products;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB
{
	public class ProductsRepository : IRepository<Product>, IProductRepository
	{
		public bool Create(Product item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Product item)
		{
			throw new NotImplementedException();
		}

		public List<Product> GetAll()
		{
			throw new NotImplementedException();
		}

		public List<Product> GetAllNotHide()
		{
			throw new NotImplementedException();
		}

		public Product GetT(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Product item)
		{
			throw new NotImplementedException();
		}
	}
}
