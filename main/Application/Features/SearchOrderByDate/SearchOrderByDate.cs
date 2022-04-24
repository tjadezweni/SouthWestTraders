using Application.Exceptions;
using Application.Features.ViewOrders;
using Application.Infrastructure.Repositories.Orders;
using MediatR;

namespace Application.Features.SearchOrderByDate
{
    public class SearchOrderByDate
    {
        public record Query : IRequest<ViewOrderDto>
        {
            public DateTime CreatedDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, ViewOrderDto>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<ViewOrderDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.SearchOrderByDate(request.CreatedDate);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.CreatedDate);
                }
                var viewOrderDto = new ViewOrderDto
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
