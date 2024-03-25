using Microsoft.EntityFrameworkCore;
using Inochis.Business.Abstract;
using Inochis.Data.Abstract;
using Inochis.Entity.Concrete;
using Inochis.Shared.ResponseViewModels;
using Inochis.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inochis.Business.Concrete
{
    public class ShoppingCartItemManager : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartItemManager(IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Response<NoContent>> ChangeQuantityAsync(int shoppingCartItemId, int quantity)
        {
            ShoppingCartItem shoppingCartItem = await _shoppingCartItemRepository.GetByIdAsync(x=>x.Id==shoppingCartItemId);
            await _shoppingCartItemRepository.ChangeQuantityAsync(shoppingCartItem, quantity);
            return Response<NoContent>.Success();
        }

        public async Task<Response<NoContent>> ClearShoppingCartAsync(int shoppingCartId)
        {

            var cart = await _shoppingCartRepository.GetByIdAsync(x=>x.Id== shoppingCartId,
                source=>source
                    .Include(x=>x.ShoppingCartItems)
                );
            cart.ShoppingCartItems = new List<ShoppingCartItem>();
            await _shoppingCartRepository.UpdateAsync(cart);
            return Response<NoContent>.Success();
        }

        public async Task<int> CountAsync(int shoppingCartId)
        {
            return await _shoppingCartItemRepository.GetCount(x=>x.ShoppingCartId==shoppingCartId);
        }

        public async Task<Response<NoContent>> DeleteFromShoppingCartAsync(int shoppingCartItemId)
        {

            var deletedCart = await _shoppingCartItemRepository.GetByIdAsync(x=>x.Id== shoppingCartItemId);
            await _shoppingCartItemRepository.HardDeleteAsync(deletedCart);
            return Response<NoContent>.Success();
        }

        public async Task<ShoppingCartItemViewModel> GetShoppingCartItemAsync(int shoppingCartItemId)
        {
            var shoppingCartItem = await _shoppingCartItemRepository.GetByIdAsync(x=>x.Id== shoppingCartItemId, source=>source
                .Include(x=>x.Product)
            );
            return new ShoppingCartItemViewModel
            {
                Id = shoppingCartItemId,
                ProductId = shoppingCartItem.ProductId,
                ProductImageUrl = shoppingCartItem.Product.ImageUrl,
                ProductName = shoppingCartItem.Product.Name,
                ProductPrice = shoppingCartItem.Product.Price,
                Quantity = shoppingCartItem.Quantity
            };
        }
    }
}
