using Application.Exceptions;
using Application.Features.ViewOrders;
using Application.Infrastructure.Repositories.Orders;
using MediatR;

namespace Application.Features.SearchOrderByDate
{
    public class SearchOrdersByDate
    {
        public record Query : IRequest<IEnumerable<OrderDto>>
        {
            public DateOnly CreatedDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<OrderDto>>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<IEnumerable<OrderDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ordersList = await _orderRepository.SearchOrdersByDate(request.CreatedDate);
                var ordersDtoList = ordersList.Select(order => new OrderDto
                {
                    OrderId = order.OrderId,
                    ProductId = order.ProductId,
                    Name = order.Name,
                    Quantity = order.Quantity,
                    CreatedDateUtc = order.CreatedDateUtc,
                    OrderState = order.OrderState.State
                });
                return ordersDtoList;
            }
        }
    }
}
