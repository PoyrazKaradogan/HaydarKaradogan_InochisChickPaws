using Inochis.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Abstract
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetAllOrdersByProductIdAsync(int productId);
    }
}
