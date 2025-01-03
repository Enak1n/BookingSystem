using BookingSystem.Infrastructure.Entities.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class BrokerMessageConfiguration : IEntityTypeConfiguration<BrokerMessage>
    {
        public void Configure(EntityTypeBuilder<BrokerMessage> builder)
        {
            builder.ToTable("PaymentStatuses");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v)
                )
                .IsRequired();
        }
    }
}
