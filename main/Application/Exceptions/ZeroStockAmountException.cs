namespace Application.Exceptions;

public class ZeroStockAmountException : ApiException
{
    public ZeroStockAmountException()
        : base(DefaultMessage())
    {

    }

    private static string DefaultMessage()
    {
        return "The provided stock amount provided (0) is invalid.";
    }
}