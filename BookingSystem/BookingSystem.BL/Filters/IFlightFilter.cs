using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Filters
{
    public interface IFlightFilter
    {
        Task<IQueryable<Flight>> FilterFlights(IQueryable<Flight> query, FlightFilterParams filterParams);
    }
}
