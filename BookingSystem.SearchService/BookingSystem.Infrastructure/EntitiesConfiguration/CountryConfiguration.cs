using BookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.EntitiesConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasDefaultValueSql("uuid_generate_v4()")
                   .IsRequired();

            builder.HasMany(x => x.Airports)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .IsRequired();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp with time zone");
        }
    }
}
