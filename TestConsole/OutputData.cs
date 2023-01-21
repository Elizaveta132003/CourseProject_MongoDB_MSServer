using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
	public static class OutputData<T>
	{
		public static void OutputList(List<T> values)
			=> values.ForEach(x => Console.WriteLine(x));

		public static void OutputObj(T obj)
			=> Console.WriteLine(obj);
	}
}
