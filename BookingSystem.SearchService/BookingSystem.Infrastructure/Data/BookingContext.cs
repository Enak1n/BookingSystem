using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Entities;
using BookingSystem.Infrastructure.Entities.Outbox;
using BookingSystem.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data
{
    public class BookingContext : DbContext, IUnitOfWork
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<PlaneEntity> Planes => Set<PlaneEntity>();
        public DbSet<FlightEntity> Flights => Set<FlightEntity>();
        public DbSet<CountryEntity> Cuntries => Set<CountryEntity>();
        public DbSet<AirportEntity> Airports => Set<AirportEntity>();
        public DbSet<BrokerMessage> Messages => Set<BrokerMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaneConfiguration());
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new AirportConfiguration());
            modelBuilder.ApplyConfiguration(new BrokerMessageConfiguration());

            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
