namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(string entityType, int entityId)
            : base(DefaultMessage(entityType, entityId))
        {

        }

        private static string DefaultMessage(string entityType, int entityId)
        {
            return $"The {entityType} with id: {entityId} was not found.";
        }
    }
}
