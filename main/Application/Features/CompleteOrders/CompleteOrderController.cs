using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.CompleteOrders
{
    [Route("api/orders")]
    [ApiController]
    public class CompleteOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompleteOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("/complete")]
        public async Task<IActionResult> PlaceOrder([FromBody] CompleteOrder.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
