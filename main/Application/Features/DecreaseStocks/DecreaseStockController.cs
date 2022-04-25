using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.DecreaseStocks
{
    [Route("api/products")]
    public class DecreaseStockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DecreaseStockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{productId:int}/stock/decrease/{amount:int}")]
        [SwaggerOperation(Summary = "Decreases the stock amount for a product with the id provided id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The amount provided was not above zero")]
        public async Task<IActionResult> DecreaseStock(int productId, int amount)
        {
            if (amount <= 0)
                return BadRequest("The amount provided was not above zero");
            var command = new DecreaseStock.Command { ProductId = productId, StockAmount = amount };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
