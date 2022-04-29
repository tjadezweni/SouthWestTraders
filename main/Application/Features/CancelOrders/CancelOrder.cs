using Application.Exceptions;
using Application.Infrastructure.Repositories.Orders;
using Application.Infrastructure.Repositories.Stocks;
using Application.Infrastructure.SeedWork;
using Application.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.CancelOrders
{
    public static class CancelOrder
    {
        public record Command : IRequest<Unit>
        {
            [Required]
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IOrderRepository _orderRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IStockRepository stockRepository,
                IOrderRepository orderRepository,
                IUnitOfWork unitOfWork)
            {
                _stockRepository = stockRepository;
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
                if (order.OrderStateId == (int)OrderState.COMPLETED)
                {
                    throw new OrderCompleteException();
                }
                var stock = await _stockRepository.GetStockWithProductId(order.ProductId);
                stock!.IncreaseStockBy(order.Quantity);
                await _stockRepository.UpdateAsync(stock);
                order.OrderStateId = (int)OrderState.CANCELLED;
                await _orderRepository.UpdateAsync(order);
                await _unitOfWork.CompleteAsync();
                return Unit.Value;
            }
        }
    }
}
