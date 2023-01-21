using Services.Implementations.Validations;
using Services.Interfaces.Validations;
using Xunit;

namespace AutomaticTest
{
	public class UserValidationTest
	{
		[Theory]
		[InlineData("Tifest")]
		[InlineData("T")]
		public void IsValidNameOrganization_NameOrganizationEnteredCorrectly_True(string nameOrganization)
		{
			//Arrange

			IAuthorizationValidation authorizationValidation = new AuthorizationValidation();

			//Act

			var result = authorizationValidation.NameOrganizationValide(nameOrganization);

			//Assert

			Assert.True(result);
		}

		[Theory]
		[InlineData("123")]
		[InlineData("%!$@")]
		[InlineData("1327^!#@#")]
		[InlineData("rrrrrrrrr")]
		public void IsValidNameOrganization_NameOrganizationEnteredIncorrecr_False(string nameOrganization)
		{
			//Arrange


			IAuthorizationValidation authorizationValidation = new AuthorizationValidation();


			//Act

			var result = authorizationValidation.NameOrganizationValide(nameOrganization);

			//Assert

			Assert.False(result);
		}

		[Theory]
		[InlineData("+375440000000")]
		[InlineData("+375290000000")]
		[InlineData("+375330000000")]
		public void IsValidPhoneNumber_PhoneNumberEnteredCorrect_True(string phoneNumber)
		{
			//Arrange
			IAuthorizationValidation authorizationValidation = new AuthorizationValidation();


			//Act

			var result = authorizationValidation.PhoneNumberValidate(phoneNumber);

			//Assert

			Assert.True(result);
		}

		[Theory]
		[InlineData("375440000000")]
		[InlineData("+0000050085")]
		[InlineData("oysf")]
		[InlineData("+375110000000")]
		[InlineData("000")]
		public void IsValidPhoneNumber_PhoneNumberEnteredIncorrect_False(string phoneNumber)
		{
			//Arrange
			IAuthorizationValidation authorizationValidation = new AuthorizationValidation();

			//Act

			var result = authorizationValidation.PhoneNumberValidate(phoneNumber);

			//Assert

			Assert.False(result);
		}
	}
}
