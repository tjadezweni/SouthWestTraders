namespace Application.Exceptions
{
    public class OrderCancelledException : ApiException
    {
        public OrderCancelledException()
            : base(DefaultMessage())
        {

        }

        private static string DefaultMessage()
        {
            return "An order that's been cancelled cannot be completed";
        }
    }
}
