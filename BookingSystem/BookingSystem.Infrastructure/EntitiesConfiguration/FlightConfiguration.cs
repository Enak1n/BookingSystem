using BookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class FlightConfiguration : IEntityTypeConfiguration<FlightEntity>
    {
        public void Configure(EntityTypeBuilder<FlightEntity> builder)
        {
            builder.ToTable("Flights");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
