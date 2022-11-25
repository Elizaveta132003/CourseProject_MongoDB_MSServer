using Domain.Core.Models;
using Domain.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
    public interface ICommodityExpertService
    {
        BaseResponse<List<Order>> GetAllOrders();
        BaseResponse<bool> UpdateOrders(Order order);
        BaseResponse<bool> CreateInvoice(Invoice invoice);

    }
}
