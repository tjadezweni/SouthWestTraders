using Application.Exceptions;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.OrderStates;
using Application.Infrastructure.Repositories.Stocks;
using MediatR;

namespace Application.Features.PlaceOrders
{
    public static class PlaceOrder
    {
        public record Command : IRequest<Unit>
        {
            public int ProductId { get; set; }
            public string OrderName { get; set; } = null!;
            public int Quantity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IOrderStateRepository _orderStateRepository;
            private readonly IOrderRepository _orderRepository;

            public Handler(IStockRepository stockRepository, 
                IOrderStateRepository orderStateRepository,
                IOrderRepository orderRepository)
            {
                _stockRepository = stockRepository;
                _orderStateRepository = orderStateRepository;
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
                    throw new Exception("Cannot place order for quantity more than available stock");
                }
                stock.AvailableStock -= request.Quantity;
                await _stockRepository.UpdateAsync(stock);
                var order = new Order { Name = request.OrderName, Quantity = request.Quantity, OrderStateId = 1 };
                await _orderRepository.AddAsync(order);
                return Unit.Value;
            }
        }
    }
}
