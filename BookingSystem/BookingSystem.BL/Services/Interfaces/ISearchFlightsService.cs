using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Services.Interfaces
{
    public interface ISearchFlightsService
    {
        Task<List<Flight>> FilterFlightsAsync(List<Flight> flights, FlightFilterParams filterParams, CancellationToken cancellationToken);
    }
}
