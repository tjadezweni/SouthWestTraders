using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.PlaceOrders
{
    [Route("api/orders")]
    public class PlaceOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlaceOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder.Command command)
        {
            await _mediator.Send(command);
            return Created("", null);
        }
    }
}
