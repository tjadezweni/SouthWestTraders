using Application.Exceptions;
using Application.Infrastructure.Repositories.Products;
using Application.Infrastructure.SeedWork;
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
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IProductRepository productRepository,
                IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(product => product.ProductId == request.ProductId);
                if (product is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                await _productRepository.DeleteAsync(product);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }
    }
}
