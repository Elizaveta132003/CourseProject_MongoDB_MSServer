using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models.Roles
{
	public class Employee : EntityBase
	{
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public int PositionCode { get; set; }

		public Employee(string phoneNumber, string password, string lastName, string firstName, string middleName, int positionCode)
		{
			PhoneNumber = phoneNumber;
			Password = password;
			LastName = lastName;
			FirstName = firstName;
			MiddleName = middleName;
			PositionCode = positionCode;
		}

		public Employee(int id, string phoneNumber, string password, string lastName, string firstName, string middleName, int positionCode)
		{
			Id = id;
			PhoneNumber = phoneNumber;
			Password = password;
			LastName = lastName;
			FirstName = firstName;
			MiddleName = middleName;
			PositionCode = positionCode;
		}

		public Employee() { }

		
	}
}
