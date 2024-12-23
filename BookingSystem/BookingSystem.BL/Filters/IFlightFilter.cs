using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Filters
{
    public interface IFlightFilter
    {
        Task<List<Flight>> FilterFlights(List<Flight> query, FlightFilterParams filterParams);
    }
}
