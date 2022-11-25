using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Roles
{
	public class Client:EntityBase, IAutorizationValidation
	{
		public string NameOrganization { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }

		public Client(string nameOrganization, string phoneNumber, string password)
		{
			NameOrganization = nameOrganization;
			PhoneNumber = phoneNumber;
			Password = password;
		}

		public Client() { }

		public Client(int id,string nameOrganization, string phoneNumber, string password)
		{
			Id= id;
			NameOrganization = nameOrganization;
			PhoneNumber = phoneNumber;
			Password = password;
		}

		public override bool Equals(object? obj)
		{
			return obj is Client client &&
				   Id == client.Id &&
				   NameOrganization == client.NameOrganization &&
				   PhoneNumber == client.PhoneNumber &&
				   Password == client.Password;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, NameOrganization, PhoneNumber, Password);
		}
	}
}
