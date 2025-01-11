using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Filters
{
    public interface IFlightFilter
    {
        List<FlightDto> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams);
    }
}
