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
    public class ShoppingCartItemRepository : GenericRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(InochisDbContext _context):base(_context)
        {
            
        }
        InochisDbContext InochisDbContext
        {
            get { return _dbContext as InochisDbContext; }
        }
        public async Task ChangeQuantityAsync(ShoppingCartItem shoppingCartItem, int quantity)
        {
            shoppingCartItem.Quantity = quantity;
            InochisDbContext.Update(shoppingCartItem);
            await InochisDbContext.SaveChangesAsync();
        }
    }
}
