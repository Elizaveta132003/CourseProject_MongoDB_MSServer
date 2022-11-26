using Domain.Core.Models.Orders;
using Domain.Core.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Validations
{
	public interface IInvoiceValidation : IIsNotNull
	{
		public bool ContainOrder(Order order);
		public bool ContainClient(Client client);
		public bool ContainEmployee(Employee employee);

	}
}
