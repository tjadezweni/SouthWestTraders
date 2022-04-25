using Application.Exceptions;
using Application.Features.ViewOrders;
using Application.Infrastructure.Repositories.Orders;
using MediatR;

namespace Application.Features.SearchOrderByName
{
    public class SearchOrderByName
    {
        public record Query : IRequest<OrderDto>
        {
            public string Name { get; set; } = null!;
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
                var order = await _orderRepository.SearchOrderByName(request.Name);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.Name);
                }
                var viewOrderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    ProductId = order.ProductId,
                    Name = order.Name,
                    Quantity = order.Quantity,
                    CreatedDateUtc = order.CreatedDateUtc,
                    OrderState = order.OrderState.State
                };
                return viewOrderDto;
            }
        }
    }
}
