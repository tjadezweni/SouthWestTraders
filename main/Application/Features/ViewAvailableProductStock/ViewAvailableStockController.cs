using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("{productId:int}/stocks", Name = "ViewAvailableStock")]
        [SwaggerOperation(Summary = "Views the available stock for a particular product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(ViewAvailableStockDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order with matching name was not found")]
        public async Task<IActionResult> ViewAvailableStock(int productId)
        {
            var query = new ViewAvailableStock.Query { ProductId = productId };
            var availableStock = await _mediator.Send(query);
            return Ok(availableStock);
        }
    }
}
