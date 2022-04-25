namespace Application.Exceptions
{
    public class ApiException : Exception
    {
        protected ApiException(string message)
            : base(message)
        {

        }
    }
}
