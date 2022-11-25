using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
	public class Invoice:EntityBase
	{
		public DateTime RegistrationDate { get; set; }
		public string NameOrganization { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public Order Order { get; set;}
		public Client Client { get; set; }
		public Employee Employee { get; set; }

		public Invoice(int id, DateTime registrationDate, string nameOrganization, string street, string houseNumber, Order order, Client client, Employee employee)
		{
			Id = id;
			RegistrationDate = registrationDate;
			NameOrganization = nameOrganization;
			Street = street;
			HouseNumber = houseNumber;
			Order = order;
			Client = client;
			Employee = employee;
		}
		public Invoice() { }
	}
}
