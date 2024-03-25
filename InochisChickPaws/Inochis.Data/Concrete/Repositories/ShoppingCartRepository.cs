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
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(InochisDbContext _context):base(_context)
        {
            
        }

        private InochisDbContext InochisDbContext
        {
            get { return _dbContext as InochisDbContext; } 
        }

        public async Task ClearShoppingCartAsync(int shoppingCartId)
        {

            var deletedShoppingCartItems = await InochisDbContext
                .ShoppingCartItems
                .Where(x=>x.ShoppingCartId== shoppingCartId)
                .ToListAsync();
            InochisDbContext.ShoppingCartItems.RemoveRange(deletedShoppingCartItems);
            await InochisDbContext.SaveChangesAsync();
        }

        public async Task DeleteFromShoppingCartAsync(int shoppingCartId, int productId)
        {
            var deletedShoppingCartItem = await InochisDbContext
                .ShoppingCartItems
                .Where(x=>x.ShoppingCartId== shoppingCartId && x.ProductId==productId)
                .FirstOrDefaultAsync();
            InochisDbContext.ShoppingCartItems.Remove(deletedShoppingCartItem);
            await InochisDbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
        {
            var shoppingCart = await InochisDbContext
                .ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .Include(sc => sc.ShoppingCartItems)
                .ThenInclude(sci => sci.Product)
                .FirstOrDefaultAsync();
            return shoppingCart;
        }
    }
}
