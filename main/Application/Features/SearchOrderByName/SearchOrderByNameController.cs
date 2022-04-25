using Application.Features.ViewOrders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.SearchOrderByName
{
    [Route("api/orders")]
    [ApiController]
    public class SearchOrderByNameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchOrderByNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search/name", Name = "SearchOrderByName")]
        [SwaggerOperation(Summary = "Searches for a order with the provided name")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(OrderDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order with matching name was not found")]
        public async Task<IActionResult> SearchOrderByName([FromQuery] string name)
        {
            var query = new SearchOrderByName.Query { Name = name.ToLower() };
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }
}
