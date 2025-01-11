using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.SearchService.Api.Extensions
{
    public static class FlightMapper
    {
        public static FlightDto ToDto(this Flight flight)
        {
            return new FlightDto
            {
                Id = flight.Id,
                DepartureDate = flight.DepartureDate,
                DeparturePoint = flight.DeparturePoint,
                DestinationPoint = flight.DestinationPoint,
                Price = flight.Price,
            };
        }
    }
}
