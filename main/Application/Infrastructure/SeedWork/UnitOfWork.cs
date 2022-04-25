namespace Application.Infrastructure.SeedWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouthWestTradersDBContext _dbContext;

        public UnitOfWork(SouthWestTradersDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
