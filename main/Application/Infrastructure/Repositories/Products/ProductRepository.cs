using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Products
{
    public class ProductRepository : AsyncRepository<Product>, IProductRepository
    {
        public ProductRepository(SouthWestTradersDBContext dbContext)
            : base(dbContext)
        {

        }
    }
}
