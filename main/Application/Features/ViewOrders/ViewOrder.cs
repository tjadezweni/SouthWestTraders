using Application.Exceptions;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.OrderStates;
using MediatR;

namespace Application.Features.ViewOrders
{
    public static class ViewOrder
    {
        public record Query : IRequest<OrderDto>
        {
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, OrderDto>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<OrderDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderByIdEager(request.OrderId);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.OrderId);
                }
                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    ProductId = order.ProductId,
                    Name = order.Name,
                    CreatedDateUtc = order.CreatedDateUtc,
                    Quantity = order.Quantity,
                    OrderState = order.OrderState.State
                };
                return orderDto;
            }
        }
    }
}
