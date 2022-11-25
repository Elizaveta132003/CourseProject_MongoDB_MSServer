using Services.Interfaces.Validations;

namespace Services.Implementations.Validations
{
	public class BankValidation : IBankValidation
	{
		public bool CheckingAvailabilityMoneyOnTheCard(long numberCard)
			=> new Random().Next(0, 2) == 1;

		public bool CheckingExistenceCard(long numberCard)
			=> new Random().Next(0, 2) == 1;
	}
}
