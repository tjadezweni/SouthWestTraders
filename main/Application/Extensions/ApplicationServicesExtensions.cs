using Application.Infrastructure;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.OrderStates;
using Application.Infrastructure.Repositories.Products;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void RegisterDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SouthWestTradersDBContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DbConnection"));
            });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderStateRepository, OrderStateRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
