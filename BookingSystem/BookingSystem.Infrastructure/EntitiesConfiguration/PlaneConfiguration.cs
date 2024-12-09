using BookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class PlaneConfiguration : IEntityTypeConfiguration<PlaneEntity>
    {
        public void Configure(EntityTypeBuilder<PlaneEntity> builder)
        {
            builder.ToTable("Planes");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Flights)
                .WithOne(x => x.Plane)
                .HasForeignKey(x => x.PlaneId)
                .IsRequired();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
