using Domain.Core.Models;
using Domain.Core.Models.Orders;
using Domain.Core.Models.Products;
using Domain.Core.Models.Roles;

namespace Services.Interfaces.Repositories
{
    public interface IUserService
    {
        BaseResponse<List<Product>> GetAllProduct();
        BaseResponse<Client> InformationAboutClient(Client client);
        BaseResponse<bool> UpdateInformationAboutClient(Client client);
        //BaseResponse<List<Order>> GetAllUserOrder(Client client);
        BaseResponse<bool> PlaceAnOrder(Order order);
    }
}
