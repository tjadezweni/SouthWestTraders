using Application.Exceptions;
using Application.Features.AddProducts;
using Application.Infrastructure.Repositories.Products;
using MediatR;

namespace Application.Features.SearchProductsByName
{
    public static class SearchProductByName
    {
        public record Query : IRequest<ProductDto>
        {
            public string Name { get; set; } = null!;
        }

        public class Handler : IRequestHandler<Query, ProductDto>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<ProductDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var searchName = request.Name.ToLower();
                var product = await _productRepository.GetAsync(product => product.Name.ToLower().Contains(searchName));
                if (product is null)
                {
                    throw new ProductNotFoundException(searchName);
                }
                var productDto = new ProductDto { ProductId = product.ProductId, Name = product.Name, 
                    Description = product.Description, Price = product.Price };
                return productDto;
            }
        }
    }
}
