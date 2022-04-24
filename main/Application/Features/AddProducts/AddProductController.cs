using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.AddProducts
{
    [Route("/api/products")]
    public class AddProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddProductController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProduct.Command command)
        {
            var product = await _mediator.Send(command);
            return Created("", product);
        }
    }
}
