using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrderByName([FromQuery] string name)
        {
            var query = new SearchOrderByName.Query { Name = name };
            var product = await _mediator.Send(query);
            return Ok(product);
        }
    }
}
