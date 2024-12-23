using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.Api.DTO
{
    public record FilterRequest(FlightFilterParams FilterParams, List<Flight> Flights);
}
