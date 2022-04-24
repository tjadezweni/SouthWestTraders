using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.ViewAvailableProductStock
{
    [Route("api/products")]
    public class ViewAvailableStockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewAvailableStockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{productId:int}/stocks")]
        public async Task<IActionResult> ViewAvailableStock(int productId)
        {
            var query = new ViewAvailableStock.Query { ProductId = productId };
            var availableStock = await _mediator.Send(query);
            return Ok(availableStock);
        }
    }
}
