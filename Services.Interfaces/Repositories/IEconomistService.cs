using Domain.Core.Models;
using Domain.Core.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
    public interface IEconomistService
    {
        BaseResponse<List<Product>> GetAllProducts();
        BaseResponse<bool> UpdateProduct(Product product);
        BaseResponse<bool> CreateProduct(Product product);
        BaseResponse<bool> DeleteProduct(Product product);

    }
}
