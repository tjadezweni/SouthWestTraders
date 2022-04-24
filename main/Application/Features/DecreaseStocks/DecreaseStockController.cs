using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> IncreaseStock(int productId, int amount)
        {
            var command = new DecreaseStock.Command { ProductId = productId, StockAmount = amount };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
