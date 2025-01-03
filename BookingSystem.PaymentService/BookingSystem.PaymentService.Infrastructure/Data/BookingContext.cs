using BookingSystem.PaymentService.Domain.SeedWork;
using BookingSystem.PaymentService.Infrastructure.Entities;
using BookingSystem.PaymentService.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.PaymentService.Infrastructure.Data
{
    public class BookingContext : DbContext, IUnitOfWork
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<TicketEntity> Tickets => Set<TicketEntity>();
        public DbSet<PaymentStatus> PaymentStatuses => Set<PaymentStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TicketEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentStatusConfiguration());

            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
