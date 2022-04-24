using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RemoveProducts
{
    [Route("api/products")]
    public class RemoveProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemoveProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var command = new RemoveProduct.Command { ProductId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
