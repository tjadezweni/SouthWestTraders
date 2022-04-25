using Application.ActionFilters;
using Application.Features.ViewOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.SearchOrderByDate
{
    [Route("api/orders")]
    public class SearchOrdersByDateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchOrdersByDateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search/date", Name = "SearchOrdersById")]
        [SwaggerOperation(Summary = "Searches for orders that occurred on a specific date")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(List<OrderDto>))]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchOrderByDate([FromQuery] SearchOrdersByDate.Query query)
        {
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }
}
