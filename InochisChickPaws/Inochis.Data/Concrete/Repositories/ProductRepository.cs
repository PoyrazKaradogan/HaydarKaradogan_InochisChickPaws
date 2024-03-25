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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(InochisDbContext _context) : base(_context)
        {

        }
        private InochisDbContext InochisDbContext
        {
            get { return _dbContext as InochisDbContext; }
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            List<Product> products = await InochisDbContext
                .Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();
            return products;
        }

        public async Task ClearProductCategory(int productId, List<int> categoryIds)
        {
            List<ProductCategory> productCategories = await InochisDbContext
                .ProductCategories
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();
            InochisDbContext.ProductCategories.RemoveRange(productCategories);
            
            await InochisDbContext.SaveChangesAsync();
        }
    }
}
