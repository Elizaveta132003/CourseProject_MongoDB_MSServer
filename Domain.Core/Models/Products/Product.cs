using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Products
{
	public class Product:EntityBase
	{

		public string Name { get; set; }
		public int Type { get; set; }
		public decimal Price { get; set; }

		public Product( string name, int type, decimal price)
		{
			//Id = id;
			Name = name;
			Type = type;
			Price = price;
		}
		public Product() { }
		public Product(int id, string name, int type, decimal price)
		{
			Id = id;
			Name = name;
			Type = type;
			Price = price;
		}

		public override bool Equals(object? obj)
		{
			return obj is Product product &&
				   Id == product.Id &&
				   Name == product.Name &&
				   Type == product.Type &&
				   Price == product.Price;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Name, Type, Price);
		}
	}
}
