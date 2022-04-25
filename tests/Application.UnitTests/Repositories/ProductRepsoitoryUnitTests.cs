using Application.Infrastructure;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Repositories
{
    public class ProductRepsoitoryUnitTests
    {
        private readonly IProductRepository productRepository;
        private readonly SouthWestTradersDBContext dbContext;

        public ProductRepsoitoryUnitTests()
        {
            dbContext = MockDbContext.GetContext();
            productRepository = new ProductRepository(dbContext);
        }

        [Theory]
        [InlineData("Coke", "A 250ml fizzy drink", 12.50)]
        [InlineData("Waffles", "A delicious desert with ice cream", 49.99)]
        [InlineData("Snickers", "A chocolate bar", 9.99)]
        public async Task AddAsync_ShouldAddProducts(string name, string description, decimal price)
        {
            // Arrange
            var productToBeAdded = new Product { Name = name, Description = description, Price = price };

            // Act
            var product = await productRepository.AddAsync(productToBeAdded);
            await dbContext.SaveChangesAsync();

            // Assert
            Assert.NotNull(product);
            Assert.Same(name, product.Name);
            Assert.Same(description, product.Description);
            Assert.Equal(price, product.Price);
        }

        [Theory]
        [InlineData("Burger", "Typical cheese burger", 145.00)]
        [InlineData("Pasta", "Basic italian pasta", 85.00)]
        [InlineData("Lasagne", "Wow", 100.00)]
        public async Task UpdateAsync_ShouldUpdateProducts(string name, string description, decimal price)
        {
            // Arrange
            int productId = 1;
            var productMacbookAir = new Product { Name = "Macbook Air", Description = "A very nice laptop", Price = 899.99m };
            await dbContext.Products.AddAsync(productMacbookAir);
            await dbContext.SaveChangesAsync();

            var product = dbContext.Products.Where(product => product.ProductId == productId).FirstOrDefault();
            product.Name = name;
            product.Description = description;
            product.Price = price;
            
            // Act
            var updatedProduct = await productRepository.UpdateAsync(product);
            await dbContext.SaveChangesAsync();

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal(productId, updatedProduct.ProductId);
            Assert.Same(name, updatedProduct.Name);
            Assert.Same(description, updatedProduct.Description);
            Assert.Equal(price, updatedProduct.Price);
        }

        [Theory]
        [InlineData("Sriracha", "GOAT of sauces", 23.50)]
        [InlineData("BBQ", "Decent sauce", 20.00)]
        public async Task DeleteAsync_ShouldDeleteProducts(string name, string description, decimal price)
        {
            // Arrange
            var productToBeDeleted = new Product { Name = name, Description = description, Price = price };
            await dbContext.Products.AddAsync(productToBeDeleted);
            await dbContext.SaveChangesAsync();

            // Act
            await productRepository.DeleteAsync(productToBeDeleted);
            await dbContext.SaveChangesAsync();

            var product = dbContext.Products.Where(product => product.ProductId == productToBeDeleted.ProductId).FirstOrDefault();

            // Assert
            Assert.Null(product);
        }

        [Theory]
        [InlineData("Sriracha", "GOAT of sauces", 23.50)]
        [InlineData("BBQ", "Decent sauce", 20.00)]
        public async Task GetAsync_ShouldRetrieveAProduct(string name, string description, decimal price)
        {
            // Arrange
            var productToBeRetrieved = new Product { Name = name, Description = description, Price= price };
            await dbContext.Products.AddAsync(productToBeRetrieved);
            await dbContext.SaveChangesAsync();

            // Act
            var product = await productRepository.GetAsync(product => product.ProductId == productToBeRetrieved.ProductId);

            // Assert
            Assert.NotNull(product);
            Assert.Same(name, product.Name);
            Assert.Same(description, product.Description);
            Assert.Equal(price, product.Price);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetAsync_ShouldRetrieveNullProduct(int productId)
        {
            // Act
            var product = await productRepository.GetAsync(product => product.ProductId == productId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task ListAsync_ShouldRetrieveProductList()
        {
            // Act
            var productList = await productRepository.ListAsync(product => true);

            // Assert
            Assert.NotNull(productList);
            Assert.Empty(productList);
        }
    }
}
