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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(InochisDbContext _context) : base(_context)
        {

        }

        private InochisDbContext MiniShopDbContext
        {
            get { return _dbContext as InochisDbContext; }
        }

        public async Task<List<Category>> GetTopCategories(int n)
        {
            List<Category> categories = await MiniShopDbContext
                .Categories
                .Where(c=>c.IsActive && !c.IsDeleted)
                .Take(n)
                .ToListAsync();
            return categories;
        }
    }
}
