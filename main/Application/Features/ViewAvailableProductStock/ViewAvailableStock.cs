using Application.Exceptions;
using Application.Infrastructure.Repositories.Stocks;
using MediatR;

namespace Application.Features.ViewAvailableProductStock
{
    public static class ViewAvailableStock
    {
        public record Query : IRequest<ViewAvailableStockDto>
        {
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ViewAvailableStockDto>
        {
            private readonly IStockRepository _stockRepository;

            public Handler(IStockRepository stockRepository)
            {
                _stockRepository = stockRepository;
            }

            public async Task<ViewAvailableStockDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var stock = await _stockRepository.GetStockWithProductId(request.ProductId);
                if (stock is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                var viewAvailableStockDto = new ViewAvailableStockDto { AvailableStock = stock.AvailableStock };
                return viewAvailableStockDto;
            }
        }
    }
}
