﻿using Application.Exceptions;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

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
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IStockRepository stockRepository,
                IUnitOfWork unitOfWork)
            {
                _stockRepository = stockRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = await _stockRepository.GetStockWithProductId(request.ProductId);
                if (stock is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                stock.IncreaseStockBy(request.StockAmount);
                await _stockRepository.UpdateAsync(stock);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }
    }
}
