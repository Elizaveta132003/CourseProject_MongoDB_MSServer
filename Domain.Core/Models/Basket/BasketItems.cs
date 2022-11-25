using Domain.Core.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Basket
{
	public class BasketItems
	{
		public Product Product { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }

		public BasketItems(Product product, int count, decimal price)
		{
			Product = product;
			Count = count;
			Price = price;
		}
	}
}
