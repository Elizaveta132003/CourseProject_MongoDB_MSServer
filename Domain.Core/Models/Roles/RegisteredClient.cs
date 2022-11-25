using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Roles
{
	public class RegisteredClient : IAutorizationValidation
	{
		public string NameOrganization { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is RegisteredClient client &&
				   NameOrganization == client.NameOrganization &&
				   PhoneNumber == client.PhoneNumber &&
				   Password == client.Password;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(NameOrganization, PhoneNumber, Password);
		}
	}
}
