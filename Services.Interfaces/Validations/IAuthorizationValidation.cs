using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Validations
{
	public interface IAuthorizationValidation
	{
		public bool PhoneNumberValidate(string phoneNumber);
		public bool PasswordIsValidate(string password);
	}
}
