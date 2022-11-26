using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using Services.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Validations
{
	public class InvoiceValidation : IInvoiceValidation
	{
		private Invoice _invoice;

		public InvoiceValidation(Invoice invoice)
		{
			_invoice = invoice;
		}

		public bool ContainClient(Client client)
		{
			if (client == null)
				return false;

		}

		public bool ContainEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}

		public bool ContainOrder(Order order)
		{
			throw new NotImplementedException();
		}
	}
}
