using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpPut("/complete", Name = "CompleteOrder")]
        [SwaggerOperation(Summary = "Completes an order for a product specified")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product with matching id was not found")]
        public async Task<IActionResult> CompleteOrder([FromBody] CompleteOrder.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
