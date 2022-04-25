namespace Application.Exceptions
{
    public class InvalidStockAmountException : Exception
    {
        public InvalidStockAmountException()
            : base()
        {

        }

        private static string DefaultMessage()
        {
            return "The provided stock amount is invalid as it results in the available stock being less than zero";
        }
    }
}
