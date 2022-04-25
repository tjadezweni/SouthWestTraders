namespace Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        protected NotFoundException(string entityType, object entityProperty)
            : base(DefaultMessage(entityType, entityProperty))
        {

        }

        private static string DefaultMessage(string entityType, object entityProperty)
        {
            return $"The {entityType} with id/name: {entityProperty} was not found.";
        }
    }
}
