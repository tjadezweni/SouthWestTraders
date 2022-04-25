using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpDelete("{id:int}", Name = "RemoveProduct")]
        [SwaggerOperation(Summary = "Removes a product to the warehouse")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Request Successful")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product (to be deleted) was not found")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var command = new RemoveProduct.Command { ProductId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
