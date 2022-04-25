using Application.Exceptions;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.SeedWork;
using Application.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.CompleteOrders
{
    public static class CompleteOrder
    {
        public record Command : IRequest<Unit>
        {
            [Required]
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IOrderRepository orderRepository,
                IUnitOfWork unitOfWork)
            {
                _orderRepository = orderRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetAsync(order => order.OrderId == request.OrderId);
                if (order is null)
                {
                    throw new OrderNotFoundException(request.OrderId);
                }
                order.OrderStateId = (int)OrderState.COMPLETED;
                await _orderRepository.UpdateAsync(order);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }
    }
}
