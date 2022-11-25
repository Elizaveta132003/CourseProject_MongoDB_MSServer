using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
	public class Stock:EntityBase
	{
		public DateTime DateOfReceipt { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DateOfDispatch { get; set; }
		public Product Product { get; set; }
		public int Count { get; set; }
		public Employee Employee { get; set; }

		public Stock(DateTime dateOfReceipt, DateTime orderDate, DateTime dateOfDispatch, Product product, int count, Employee employee)
		{
			DateOfReceipt = dateOfReceipt;
			OrderDate = orderDate;
			DateOfDispatch = dateOfDispatch;
			Product = product;
			Count = count;
			Employee = employee;
		}
		public Stock() { }
	}
}
