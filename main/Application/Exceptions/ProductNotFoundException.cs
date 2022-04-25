namespace Application.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(object productProperty)
            : base("Product", productProperty)
        {

        }
    }
}
