namespace Application.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(object orderProperty)
            : base("Order", orderProperty)
        {

        }
    }
}
