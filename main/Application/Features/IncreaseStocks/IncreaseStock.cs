using Application.Exceptions;
using Application.Infrastructure.Repositories.Stocks;
using MediatR;

namespace Application.Features.IncreaseStocks
{
    public static class IncreaseStock
    {
        public record Command : IRequest<Unit>
        {
            public int ProductId { get; set; }
            public int StockAmount { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IStockRepository _stockRepository;

            public Handler(IStockRepository stockRepository)
            {
                _stockRepository = stockRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = await _stockRepository.GetStockWithProductId(request.ProductId);
                if (stock is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                int newStockAmount = stock.AvailableStock + request.StockAmount;
                stock.AvailableStock = newStockAmount;
                await _stockRepository.UpdateAsync(stock);
                return Unit.Value;
            }
        }
    }
}
