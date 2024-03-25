using Inochis.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Abstract
{
    public interface IShoppingCartRepository:IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
        Task DeleteFromShoppingCartAsync(int shoppingCartId, int productId);
        Task ClearShoppingCartAsync(int shoppingCartId);
    }
}
