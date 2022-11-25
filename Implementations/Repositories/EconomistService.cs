using Domain.Core.Models;
using Domain.Core.Models.Products;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Repositories;

namespace Services.Implementations.Repositories
{
    public class EconomistService : IEconomistService
	{

		public IRepository<Product> ProductRepository { get; set; }

		public EconomistService(IRepository<Product> productRepository)
		{
			ProductRepository = productRepository;
		}

		public BaseResponse<bool> CreateProduct(Product product)
		{
			try
			{
				if (product == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				ProductRepository.Create(product);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch(Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> DeleteProduct(Product product)
		{
			try
			{
				if(product == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				ProductRepository.Delete(product);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch(Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<List<Product>> GetAllProducts()
		{
			try
			{
				var data=ProductRepository.GetAll();

				return new BaseResponse<List<Product>>()
				{
					Data = data,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch(Exception ex)
			{
				return new BaseResponse<List<Product>>()
				{
					Description = ex.Message,
					StatusCode = new NotFoundResult()
				};
			}
		}

		public BaseResponse<bool> UpdateProduct(Product product)
		{
			try
			{
				if (product == null)
				{
					return new BaseResponse<bool>()
					{
						Data = false,
						Description = "Not found",
						StatusCode = new NotFoundResult()
					};
				}

				ProductRepository.Update(product);

				return new BaseResponse<bool>()
				{
					Data = true,
					Description = "Completed successfully",
					StatusCode = new OkResult()
				};
			}
			catch(Exception ex)
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
