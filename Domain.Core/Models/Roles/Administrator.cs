using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Roles
{
	public class Administrator:Employee
	{
		
		public Administrator() { }

		public Administrator(string phoneNumber, string password, string lastName, string firstName, string middleName, int positionCode) : base(phoneNumber, password, lastName, firstName, middleName, positionCode)
		{
		}
	}
}
