using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> Post([FromBody] AddProduct.Command command)
        {
            var product = await _mediator.Send(command);
            return Created("", product);
        }
    }
}
