using Application.Exceptions;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.OrderStates;
using MediatR;

namespace Application.Features.ViewOrders
{
    public static class ViewOrder
    {
        public record Query : IRequest<ViewOrderDto>
        {
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ViewOrderDto>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderStateRepository _orderStateRepository;

            public Handler(IOrderRepository orderRepository,
                IOrderStateRepository orderStateRepository)
            {
                _orderRepository = orderRepository;
                _orderStateRepository = orderStateRepository;
            }

            public async Task<ViewOrderDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetAsync(order => order.OrderId == request.OrderId);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.OrderId);
                }
                var orderDto = new ViewOrderDto
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
