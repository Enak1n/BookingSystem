using BookingSystem.SearchService.BL.Models;

namespace BookingSystem.SearchService.Api.DTO
{
    public record CreatePaymentDto(Guid FlightId, PassengerBrokerDto Passenger);
}
