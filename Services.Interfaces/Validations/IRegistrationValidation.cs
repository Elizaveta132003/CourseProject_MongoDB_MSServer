using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Validations
{
	public interface IRegistrationValidation
	{
		public bool NameOrganizationValide(string nameOrganization);
		public bool PhoneNumberValide(string phoneNumber);
		public bool PasswordValide(string password);
	}
}
