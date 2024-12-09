using BookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class AirportConfiguration : IEntityTypeConfiguration<AirportEntity>
    {
        public void Configure(EntityTypeBuilder<AirportEntity> builder)
        {
            builder.ToTable("Airports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
