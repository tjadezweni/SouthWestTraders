namespace Application.Exceptions
{
    public class OrderFoundException : FoundException
    {
        public OrderFoundException(object orderProperty)
            : base("Order", orderProperty)
        {

        }
    }
}
