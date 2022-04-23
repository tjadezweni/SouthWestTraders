namespace SouthWestTraders.Infrastructure.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
