using Application.Features.AddProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("search/name", Name = "SearchProductByName")]
        [SwaggerOperation(Summary = "Searches for a product with the provided name")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product with matching name was not found")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string name)
        {
            var query = new SearchProductByName.Query { Name = name };
            var product = await _mediator.Send(query);
            return Ok(product);
        }

    }
}
