using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.ViewProducts
{
    [Route("api/products")]
    [ApiController]
    public class ViewProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProducts()
        {
            var query = new ViewProducts.Query();
            var productsList = await _mediator.Send(query);
            return Ok(productsList);
        }
    }
}
