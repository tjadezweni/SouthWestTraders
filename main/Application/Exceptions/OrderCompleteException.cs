namespace Application.Exceptions
{
    public class OrderCompleteException : ApiException
    {
        public OrderCompleteException()
            : base(DefaultMessage())
        {

        }

        private static string DefaultMessage()
        {
            return "An order that's been completed cannot be cancelled";
        }
    }
}
