using Services.Interfaces.Validations;
using System.Text.RegularExpressions;

namespace Services.Implementations.Validations
{
	public class AuthorizationValidation : IAuthorizationValidation
	{
		public bool PasswordIsValidate(string password)
			=> new Regex(@"[0-9a-zA-Z]{8}").IsMatch(password);

		public bool PhoneNumberValidate(string phoneNumber)
			=> new Regex(@"^\+375(33|29|25|44)([0-9]{7})").IsMatch(phoneNumber);
		public bool NameOrganizationValide(string nameOrganization)
		=> new Regex(@"^[a-zA-Z]{1,8}$").IsMatch(nameOrganization);
	}
}