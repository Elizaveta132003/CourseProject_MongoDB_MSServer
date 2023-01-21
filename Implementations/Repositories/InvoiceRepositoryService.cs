using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Repositories
{
	public class InvoiceRepositoryService : IInvoiceRepositoryService
	{
		public IRepository<Invoice> InvoiceRepository { get; set; }

		public InvoiceRepositoryService(IRepository<Invoice> invoiceRepository)
		{
			InvoiceRepository = invoiceRepository;
		}

		public BaseResponse<bool> CreateInvoice(Invoice invoice)
		{
			try
			{
				if (invoice == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				var data = InvoiceRepository.Create(invoice);

				return new BaseResponse<bool>()
				{
					Data = data,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = true,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> DeleteInvoice(int id)
		{
			try
			{
				if (id == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				InvoiceRepository.Delete(id);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<List<Invoice>> GetAllInvoice()
		{

			try
			{
				var data = InvoiceRepository.GetAll();

				return new BaseResponse<List<Invoice>>()
				{
					Data = data,
					Description = "Completed secessfully",
					StatusCode = new OkResult()
				};

			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Invoice>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> UpdateInvoice(Invoice invoice)
		{
			try
			{
				var value = InvoiceRepository.GetT(invoice.Id);

				if (value == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				InvoiceRepository.Update(invoice);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}
	}
}
