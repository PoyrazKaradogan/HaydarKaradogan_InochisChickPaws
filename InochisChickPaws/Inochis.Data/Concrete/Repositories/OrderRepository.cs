using Microsoft.EntityFrameworkCore;
using Inochis.Data.Abstract;
using Inochis.Data.Concrete.Contexts;
using Inochis.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Concrete.Repositories
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(InochisDbContext _context):base(_context)
        {
            
        }
        InochisDbContext InochisDbContext { get{ return _dbContext as InochisDbContext;  } }

        public async Task<List<Order>> GetAllOrdersByProductIdAsync(int productId)
        {
            var result = await InochisDbContext
               .Orders
               .Include(o=>o.OrderDetails)
               .ThenInclude(od=>od.Product)
               .Where(o=>o.OrderDetails.Any(x=>x.ProductId==productId))
               .OrderByDescending(x=>x.Id)
               .ToListAsync();
            return result;
        }
    }
}
