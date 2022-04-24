using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Products;
using Application.Infrastructure.Repositories.Stocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.AddProducts
{
    public static class AddProduct
    {
        public record Command : IRequest<ProductDto>
        {
            [MinLength(3)]
            [MaxLength(25)]
            public string Name { get; set; } = null!;

            [Required]
            [MinLength(5)]
            [MaxLength(50)]
            public string Description { get; set; } = null!;

            [Required]
            [Precision(8, 2)]
            [Range(0, 999999.99)]
            public decimal Price { get; set; }
        }

        public class Handler : IRequestHandler<Command, ProductDto>
        {
            private readonly IProductRepository _productRepository;
            private readonly IStockRepository _stockRepository;

            public Handler(IProductRepository productRepository, IStockRepository stockRepository)
            {
                _productRepository = productRepository;
                _stockRepository = stockRepository;
            }

            public async Task<ProductDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = new Product { Name = request.Name, Description = request.Description, Price = request.Price };
                await _productRepository.AddAsync(product);
                var stock = new Stock { ProductId = product.ProductId, AvailableStock = 0 };
                await _stockRepository.AddAsync(stock);
                var productDto = new ProductDto { ProductId = product.ProductId, Name = product.Name, 
                    Description = product.Description, Price = product.Price };
                return productDto;
            }
        }
    }
}
