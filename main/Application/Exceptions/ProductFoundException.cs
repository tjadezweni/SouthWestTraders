namespace Application.Exceptions
{
    public class ProductFoundException : FoundException
    {
        public ProductFoundException(object productProperty)
            : base("Product", productProperty)
        {

        }
    }
}
