using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Validations
{
	public static class IIsNotNull
	{
		public static bool IsNotNull(object obj)
		{
			if (obj == null || obj == "")
				return false;
			return true;
		}
	}
}
