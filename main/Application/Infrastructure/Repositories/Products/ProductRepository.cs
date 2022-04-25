using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories.Products
{
    public class ProductRepository : AsyncRepository<Product>, IProductRepository
    {
        public ProductRepository(SouthWestTradersDBContext dbContext)
            : base(dbContext)
        {

        }

        public Task<Product?> SearchProductByName(string name)
        {
            return _dbSet.Where(product => product.Name.ToLower().Contains(name))
                .FirstOrDefaultAsync();
        }
    }
}
