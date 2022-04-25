using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.CancelOrders
{
    [Route("api/orders")]
    public class CancelOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CancelOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("/cancel", Name = "CancelOrder")]
        [SwaggerOperation(Summary = "Cancels an order for a product specified")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product with matching id was not found")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrder.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
