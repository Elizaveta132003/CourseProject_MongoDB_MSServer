using Domain.Core.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Orders
{
	public class OrderItem
	{
		public Product Product { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }

		public OrderItem(Product product, int count)
		{
			Product = product;
			Count = count;
			Price = Product.Price * Count;
		}
		public OrderItem() { }
	}
}
