using Application.Exceptions;
using Application.Infrastructure.Repositories.Products;
using MediatR;

namespace Application.Features.RemoveProducts
{
    public static class RemoveProduct
    {
        public record Command : IRequest<Unit>
        {
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(product => product.ProductId == request.ProductId);
                if (product is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                await _productRepository.DeleteAsync(product);
                return Unit.Value;
            }
        }
    }
}
