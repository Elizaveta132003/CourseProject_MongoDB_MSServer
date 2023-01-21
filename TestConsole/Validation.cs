using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
	public static class Validation
	{
		public static bool ObjIsNumber(string number)
			=> int.TryParse(number, out int result);

		public static bool NumberIsPositive(string number)
		{
			if (double.TryParse(number, out double result))
			{
				if (result > 0) return true;
				return false;
			}

			return false;
		}

		public static bool NumberIsInt(int number)
			=> number % 1 == 0;
		public static bool ObjIsData(string obj)
			=> DateTime.TryParse(obj, out DateTime result);
	}
}
