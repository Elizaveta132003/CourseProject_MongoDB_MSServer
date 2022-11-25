using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Roles
{
	public class Economist:Employee
	{
		
		public Economist() { }

		public Economist(string phoneNumber, string password, string lastName, string firstName, string middleName, int positionCode) : base(phoneNumber, password, lastName, firstName, middleName, positionCode)
		{
		}
	}
}
