using Application.Features.AddProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.ViewProductsList
{
    [Route("api/products")]
    public class ViewProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "ViewProducts")]
        [SwaggerOperation(Summary = "Gets a list of all the products in the warehouse")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(List<ProductDto>))]
        public async Task<IActionResult> ViewProducts()
        {
            var query = new ViewProducts.Query();
            var productsList = await _mediator.Send(query);
            return Ok(productsList);
        }
    }
}
