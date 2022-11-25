using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Validations
{
	public interface IBankValidation
	{
		public bool CheckingAvailabilityMoneyOnTheCard(long numberCard);
		public bool CheckingExistenceCard(long numberCard);
	}
}
