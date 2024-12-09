using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookingSystem.Infrastructure
{
    public class ContextFactory : IDesignTimeDbContextFactory<BookingContext>
    {
        public BookingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BookingSystem.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<BookingContext>();

            var connectionString = configuration.GetConnectionString("DbConnection");

            dbContextBuilder.UseNpgsql(connectionString);

            return new BookingContext(dbContextBuilder.Options);
        }
    }
}
