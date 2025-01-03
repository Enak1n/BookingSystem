using AutoMapper;
using BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.PaymentService.Infrastructure.Entities;

namespace BookingSystem.PaymentService.Infrastructure.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<TicketEntity, Ticket>();
        }
    }
}
