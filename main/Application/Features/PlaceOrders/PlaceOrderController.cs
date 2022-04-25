using Application.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpPost(Name = "PlaceOrder")]
        [SwaggerOperation(Summary = "Places an order for a product specified")]
        [SwaggerResponse(StatusCodes.Status201Created, "Request Successful")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product with matching id was not found")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder.Command command)
        {
            await _mediator.Send(command);
            return Created("", null);
        }
    }
}
