using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Features.IncreaseStocks
{
    [Route("api/products")]
    public class IncreaseStockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncreaseStockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{productId:int}/stock/increase/{amount:int}")]
        public async Task<IActionResult> IncreaseStock(int productId, int amount)
        {
            var command = new IncreaseStock.Command { ProductId = productId, StockAmount = amount };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
