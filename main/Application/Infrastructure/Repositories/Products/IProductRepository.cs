using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Products
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<Product?> SearchProductByName(string name);
    }
}
