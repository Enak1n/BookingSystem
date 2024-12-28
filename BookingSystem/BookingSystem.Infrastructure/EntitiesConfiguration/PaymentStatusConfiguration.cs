using BookingSystem.Infrastructure.Entities.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.ToTable("PaymentStatuses");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.PaymentEndDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP + INTERVAL '10 minutes'")
                .IsRequired();

            builder.Property(ps => ps.Status)
                .HasConversion(
                    v => v.ToString(), 
                    v => (Status)Enum.Parse(typeof(Status), v)
                )
                .IsRequired();
        }
    }
}
