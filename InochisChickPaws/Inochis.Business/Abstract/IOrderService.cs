using Inochis.Entity.Concrete;
using Inochis.Shared.ComplexTypes;
using Inochis.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Business.Abstract
{
    public interface IOrderService
    {   Task CancelOrder(int orderId);
        Task<OrderState> ChangeStatus(int id, OrderState orderState);
        Task CreateAsync(Order order);
        Task<List<AdminOrderViewModel>> GetOrdersAsync();
        Task<List<AdminOrderViewModel>> GetOrdersAsync(string userId);
        Task<List<AdminOrderViewModel>> GetOrdersAsync(int productId);
        Task<AdminOrderViewModel> GetOrderAsync(int orderId);
     
    }
}
