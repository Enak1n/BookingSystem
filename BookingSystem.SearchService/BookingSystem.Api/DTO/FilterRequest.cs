using BookingSystem.BL.Models;

namespace BookingSystem.Api.DTO
{
    public record FilterRequest(FlightFilterParams FilterParams, List<FlightDto> Flights);
}
