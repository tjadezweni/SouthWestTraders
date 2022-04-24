using Application.Exceptions;
using Application.Features.ViewOrders;
using Application.Infrastructure.Repositories.Orders;
using MediatR;

namespace Application.Features.SearchOrderByName
{
    public class SearchOrderByName
    {
        public record Query : IRequest<ViewOrderDto>
        {
            public string Name { get; set; } = null!;
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
                var searchName = request.Name.ToLower();
                var order = await _orderRepository.SearchOrderByName(searchName);
                if (order is null)
                {
                    throw new OrderNotFoundException(searchName);
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
