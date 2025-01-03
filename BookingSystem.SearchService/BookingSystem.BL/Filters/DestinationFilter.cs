using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.BL.Filters
{
    public class DestinationFilter : IFlightFilter
    {
        public async Task<List<FlightDto>> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams)
        {
            if (!string.IsNullOrEmpty(filterParams.Destination))
            {
                query = query.Where(f => f.DestinationPoint.Contains(filterParams.Destination)).ToList();
            }

            return query;
        }
    }
}
