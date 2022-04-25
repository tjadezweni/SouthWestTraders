using Application.Exceptions;
using Application.Infrastructure.Repositories.Orders;
using MediatR;

namespace Application.Features.CompleteOrders
{
    public static class CompleteOrder
    {
        public record Command : IRequest<Unit>
        {
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetAsync(order => order.OrderId == request.OrderId);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.OrderId);
                }
                order.OrderStateId = 3;
                await _orderRepository.UpdateAsync(order);
                return Unit.Value;
            }
        }
    }
}
