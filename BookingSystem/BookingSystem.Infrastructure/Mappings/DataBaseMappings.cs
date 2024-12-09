using AutoMapper;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Infrastructure.Entities;

namespace BookingSystem.Infrastructure.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<PlaneEntity, Plane>();
            CreateMap<FlightEntity, Flight>();
        }
    }
}
