using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Services.Interfaces
{
    public interface ISearchFlightsService
    {
        Task<List<FlightDto>> FilterFlightsAsync(List<FlightDto> flights, FlightFilterParams filterParams, CancellationToken cancellationToken);
    }
}
