using Domain.Core.Models;
using Domain.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repositories
{
    public interface IDispatcherService
    {
        BaseResponse<List<Order>> GetAllOrders();
        BaseResponse<bool> Update(Order order);
        BaseResponse<bool> CreatePlanOfProduce(Order order);
    }
}
