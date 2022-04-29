using Application.Exceptions;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Entities
{
    public class Stock : BaseEntity
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }

        public virtual Product? Product { get; set; }

        public void DecreaseStockBy(int stockDecreaseAmount)
        {
            ValidateStockAmount(stockDecreaseAmount);
            int newStockAmount = AvailableStock - stockDecreaseAmount;
            if (newStockAmount < 0)
            {
                throw new InvalidStockAmountException();
            }

            AvailableStock = newStockAmount;
        }

        public void IncreaseStockBy(int stockIncreaseAmount)
        {
            ValidateStockAmount(stockIncreaseAmount);
            AvailableStock += stockIncreaseAmount;
        }

        private void ValidateStockAmount(int stockAmount)
        {
            if (stockAmount > 0)
            {
                return;
            }

            throw new ZeroStockAmountException();
        }
    }
}
