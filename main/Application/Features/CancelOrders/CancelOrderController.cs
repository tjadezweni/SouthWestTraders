using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.CancelOrders
{
    [Route("api/orders")]
    [ApiController]
    public class CancelOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CancelOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("/cancel")]
        public async Task<IActionResult> PlaceOrder([FromBody] CancelOrder.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
