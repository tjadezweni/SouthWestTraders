using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.ViewOrders
{
    [Route("api/orders")]
    public class ViewOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{orderId:int}")]
        public async Task<IActionResult> ViewOrder(int orderId)
        {
            var query = new ViewOrder.Query { OrderId = orderId };
            var order = await _mediator.Send(query);
            return Ok(order);
        }
    }
}
