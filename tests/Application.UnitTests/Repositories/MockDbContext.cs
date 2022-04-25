using Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.UnitTests.Repositories
{
    internal static class MockDbContext
    {
        public static SouthWestTradersDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<SouthWestTradersDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new SouthWestTradersDBContext(options);
            return dbContext;
        }
    }
}
