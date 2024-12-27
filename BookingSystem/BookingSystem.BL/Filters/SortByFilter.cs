using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.BL.Filters
{
    public class SortByFilter : IFlightFilter
    {
        public async Task<List<FlightDto>> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams)
        {
            if (!string.IsNullOrEmpty(filterParams.SortBy))
            {
                query = filterParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DepartureDate).ToList()
                    : query.OrderBy(x => x.DepartureDate).ToList();

                return query;
            }

            return query;
        }
    }
}
