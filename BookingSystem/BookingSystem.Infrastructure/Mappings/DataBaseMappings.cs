using AutoMapper;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
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

            CreateMap<FlightEntity, Flight>()
                .ForMember(dest => dest.DeparturePoint, opt => opt.MapFrom(src => src.DeparturePoint))
                .ForMember(dest => dest.DestinationPoint, opt => opt.MapFrom(src => src.DestinationPoint))
                .ForMember(dest => dest.Plane, opt => opt.MapFrom(src => src.Plane)) // PlaneEntity → Plane
                .ForMember(dest => dest.EmptyPlaces, opt => opt.MapFrom(src => src.EmptyPlaces))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate));

            CreateMap<AirportEntity, Airport>();
            CreateMap<CountryEntity, Country>();
        }
    }
}
