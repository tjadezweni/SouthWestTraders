using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.SearchOrderByDate
{
    [Route("api/[controller]")]
    public class SearchOrderByDateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchOrderByDateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrderByName([FromQuery] DateTime date)
        {
            var query = new SearchOrderByDate.Query { CreatedDate = date };
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }
}
