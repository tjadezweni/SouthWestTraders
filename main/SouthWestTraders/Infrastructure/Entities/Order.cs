namespace SouthWestTraders.Infrastructure.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public int OrderStateId { get; set; }

        public virtual OrderState OrderState { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
