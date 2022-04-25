namespace Application.Features.ViewOrders
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public string OrderState { get; set; } = null!;
    }
}
