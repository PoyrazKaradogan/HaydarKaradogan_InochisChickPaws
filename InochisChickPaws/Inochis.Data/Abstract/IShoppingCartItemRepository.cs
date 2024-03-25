using Inochis.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Data.Abstract
{
    public interface IShoppingCartItemRepository:IGenericRepository<ShoppingCartItem>
    {
        Task ChangeQuantityAsync(ShoppingCartItem shoppingCartItem,  int quantity);
    }
}
