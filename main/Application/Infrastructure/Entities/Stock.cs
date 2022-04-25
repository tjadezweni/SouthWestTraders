using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Entities
{
    public partial class Stock : BaseEntity
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }

        public virtual Product? Product { get; set; }
    }
}
