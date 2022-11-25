using Services.Interfaces.Validations;
using System.Text.RegularExpressions;

namespace Services.Implementations.Validations
{
	public class RegistrationValidation : IRegistrationValidation
	{
		public bool NameOrganizationValide(string nameOrganization)
		=> new Regex(@"[a-zA-Zа-яА-Я]").IsMatch(nameOrganization);

		public bool PasswordValide(string password)
		=> new Regex(@"[0-9a-zA-Z]{8}").IsMatch(password);

		public bool PhoneNumberValide(string phoneNumber)
		=> new Regex(@"^((\+375)[\- ]?)?((33|29|25|44)?[\- ]?)?[\d\- ]{7}$").IsMatch(phoneNumber);
	}
}
