using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.AddProducts
{
    [Route("/api/products")]
    public class AddProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddProduct")]
        [SwaggerOperation(Summary = "Adds a product to the warehouse")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Product with matching name found")]
        public async Task<IActionResult> AddProduct([FromBody] AddProduct.Command command)
        {
            var product = await _mediator.Send(command);
            return Created("", product);
        }
    }
}
