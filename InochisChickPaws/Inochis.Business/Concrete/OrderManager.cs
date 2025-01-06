using Microsoft.EntityFrameworkCore;
using Inochis.Business.Abstract;
using Inochis.Data.Abstract;
using Inochis.Entity.Concrete;
using Inochis.Entity.Concrete.Identity;
using Inochis.Shared.ComplexTypes;
using Inochis.Shared.Extensions;
using Inochis.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task CancelOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderState> ChangeStatus(int id, OrderState orderState)
        {
            var order = await _orderRepository.GetByIdAsync(x => x.Id == id);
            order.OrderState = orderState;
            await _orderRepository.UpdateAsync(order);
            return orderState;

        }

        public async Task CreateAsync(Order order)
        {
            await _orderRepository.CreateAsync(order);
        }

        public async Task<AdminOrderViewModel> GetOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(x=>x.Id== orderId,
                source=>source
                    .Include(x=>x.OrderDetails)
                    .ThenInclude(y=>y.Product));
            var result = new AdminOrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                UserName = $"{order.FirstName} {order.LastName}",
                OrderDetails = order.OrderDetails.Select(od => new AdminOrderDetailViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Quantity = od.Quantity
                }).ToList()
            };
            return result;
        }

        public async Task<List<AdminOrderViewModel>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync(null,
                source=>source
                    .Include(x=>x.OrderDetails)
                    .ThenInclude(y=>y.Product)
                    .Include(x=>x.User));
            var result = orders.Select(o => new AdminOrderViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                UserId = o.UserId,
                UserName = $"{o.FirstName} {o.LastName}",
                OrderState = o.OrderState.GetDisplayName(),
                OrderDetails = o.OrderDetails.Select(od => new AdminOrderDetailViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Quantity = od.Quantity
                }).ToList()
            }).ToList();
            return result.OrderByDescending(x=>x.Id).ToList();
        }

        public async Task<List<AdminOrderViewModel>> GetOrdersAsync(string userId)
        {
            var orders=await _orderRepository.GetAllAsync(x=>x.UserId==userId,
                source => source
                    .Include(x => x.OrderDetails)
                    .ThenInclude(y => y.Product));
            var result = orders.Select(o => new AdminOrderViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                UserId = o.UserId,
                UserName = $"{o.FirstName} {o.LastName}",
                OrderDetails = o.OrderDetails.Select(od => new AdminOrderDetailViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Quantity = od.Quantity,
                    Product=new ProductViewModel
                    {
                        ImageUrls=od.Product.ImageUrls, Name=od.Product.Name
                    }
                }).ToList()
            }).ToList();
            result = result.OrderByDescending(x=>x.Id).ToList();
            return result;
        }

        public async Task<List<AdminOrderViewModel>> GetOrdersAsync(int productId)
        {
            var orders = await _orderRepository.GetAllOrdersByProductIdAsync(productId);
            var result = orders.Select(o => new AdminOrderViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                UserId = o.UserId,
                UserName = $"{o.FirstName} {o.LastName}",
                OrderDetails = o.OrderDetails.Select(od => new AdminOrderDetailViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Quantity = od.Quantity
                }).ToList()
            }).ToList();
            return result;
        }


    }
}
