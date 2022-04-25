using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Entities
{
    public partial class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public int OrderStateId { get; set; }

        public virtual OrderState? OrderState { get; set; }
        public virtual Product? Product { get; set; }
    }
}
