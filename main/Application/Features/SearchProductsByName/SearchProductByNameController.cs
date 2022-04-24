using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.SearchProductsByName
{
    [Route("api/products")]
    public class SearchProductByNameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchProductByNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string name)
        {
            var query = new SearchProductByName.Query { Name = name };
            var product = await _mediator.Send(query);
            return Ok(product);
        }

    }
}
