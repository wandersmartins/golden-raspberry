using GoldenRaspberry.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Tests.TestHelpers
{
    public static class DatabaseTestHelper
    {
        public static AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=:memory:") // SQLite em memória
                .Options;

            var context = new AppDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
