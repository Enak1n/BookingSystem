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
            builder.Property(x => x.Id)
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");

            builder.HasOne(x => x.DepartureAirport).WithMany(x => x.FlightsDeparting).HasForeignKey(x => x.DepartureAirportId).IsRequired(false);

            builder.HasOne(x => x.DestinationAirport).WithMany(x => x.FlightsArriving).HasForeignKey(x => x.DestinationAirportId).IsRequired(false);
        }
    }
}
