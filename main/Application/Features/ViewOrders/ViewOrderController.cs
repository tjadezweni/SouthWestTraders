using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("{orderId:int}", Name = "ViewOrder")]
        [SwaggerOperation(Summary = "Views the details of the order with the id provided")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(OrderDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order with matching id was not found")]
        public async Task<IActionResult> ViewOrder(int orderId)
        {
            var query = new ViewOrder.Query { OrderId = orderId };
            var order = await _mediator.Send(query);
            return Ok(order);
        }
    }
}
