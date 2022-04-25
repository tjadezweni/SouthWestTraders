﻿using Application.Exceptions;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.OrderStates;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using MediatR;

namespace Application.Features.PlaceOrders
{
    public static class PlaceOrder
    {
        public record Command : IRequest<Unit>
        {
            public int ProductId { get; set; }
            public string Name { get; set; } = null!;
            public int Quantity { get; set; } = 0;
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IStockRepository stockRepository, 
                IUnitOfWork unitOfWork,
                IOrderRepository orderRepository)
            {
                _stockRepository = stockRepository;
                _unitOfWork = unitOfWork;
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = await _stockRepository.GetStockWithProductId(request.ProductId);
                if (stock is null)
                {
                    throw new ProductNotFoundException(request.ProductId);
                }
                if (stock.AvailableStock < request.Quantity)
                {
                    throw new InvalidStockAmountException();
                }
                stock.AvailableStock -= request.Quantity;
                await _stockRepository.UpdateAsync(stock);
                var order = new Order { Name = request.Name, Quantity = request.Quantity, OrderStateId = 1, ProductId = request.ProductId };
                await _orderRepository.AddAsync(order);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }
    }
}
