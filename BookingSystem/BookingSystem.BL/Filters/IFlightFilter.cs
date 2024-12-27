using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Filters
{
    public interface IFlightFilter
    {
        Task<List<FlightDto>> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams);
    }
}
