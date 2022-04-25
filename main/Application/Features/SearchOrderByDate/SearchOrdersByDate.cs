using Application.Exceptions;
using Application.Features.ViewOrders;
using Application.Infrastructure.Repositories.Orders;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.SearchOrderByDate
{
    public class SearchOrdersByDate
    {
        public record Query : IRequest<IEnumerable<OrderDto>>
        {
            [Required]
            [Range(1, 31)]
            public int Day { get; set; }
            [Required]
            [Range(1, 12)]
            public int Month { get; set; }
            [Range(2022, int.MaxValue)]
            public int Year { get; set; }
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


                var ordersList = await _orderRepository.SearchOrdersByDate(request.Day, request.Month, request.Year);
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

