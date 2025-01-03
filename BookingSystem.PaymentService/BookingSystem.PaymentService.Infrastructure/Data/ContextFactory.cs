using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookingSystem.PaymentService.Infrastructure.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<BookingContext>
    {
        public BookingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BookingSystem.PaymentService.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<BookingContext>();

            var connectionString = configuration.GetConnectionString("DbConnection");

            dbContextBuilder.UseNpgsql(connectionString);

            return new BookingContext(dbContextBuilder.Options);
        }
    }
}
