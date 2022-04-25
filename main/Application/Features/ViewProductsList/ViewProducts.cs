using Application.Features.AddProducts;
using Application.Infrastructure.Repositories.Products;
using MediatR;

namespace Application.Features.ViewProductsList
{
    public static class ViewProducts
    {
        public record Query : IRequest<IEnumerable<ProductDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProductDto>>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IEnumerable<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var productsList = await _productRepository.ListAsync(product => true);
                var productsDtoList = productsList.Select(product => new ProductDto { ProductId = product.ProductId, Name = product.Name, 
                    Description = product.Description, Price = product.Price });
                return productsDtoList;
            }
        }
    }
}
