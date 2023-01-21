using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
	public interface IInvoiceRepositoryService
	{
		BaseResponse<bool> CreateInvoice(Invoice invoice);
		BaseResponse<bool> UpdateInvoice(Invoice invoice);
		BaseResponse<List<Invoice>> GetAllInvoice();
		BaseResponse<bool> DeleteInvoice(int id);
	}
}
