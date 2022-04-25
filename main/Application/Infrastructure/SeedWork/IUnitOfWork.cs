namespace Application.Infrastructure.SeedWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

    }
}
