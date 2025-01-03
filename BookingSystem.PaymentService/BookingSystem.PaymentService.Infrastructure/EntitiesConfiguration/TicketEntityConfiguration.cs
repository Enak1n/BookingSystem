﻿using BookingSystem.PaymentService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.PaymentService.Infrastructure.EntitiesConfiguration
{
    public class TicketEntityConfiguration : IEntityTypeConfiguration<TicketEntity>
    {
        public void Configure(EntityTypeBuilder<TicketEntity> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(x => x.Id);
            
            builder.OwnsOne(x => x.Passenger, passenger =>
            {
                passenger.Property(x => x.Name).HasColumnName("Name");
                passenger.Property(x => x.Surname).HasColumnName("Surname");
                passenger.Property(x => x.Patronymic).HasColumnName("Patronymic");
                passenger.Property(x => x.Email).HasColumnName("Email");
            });
        }
    }
}
