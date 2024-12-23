using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.BL.Filters
{
    public class SortByFilter : IFlightFilter
    {
        public async Task<IQueryable<Flight>> FilterFlights(IQueryable<Flight> query, FlightFilterParams filterParams)
        {
            if (!string.IsNullOrEmpty(filterParams.SortBy))
            {
                query = filterParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(f => EF.Property<object>(f, filterParams.SortBy))
                    : query.OrderBy(f => EF.Property<object>(f, filterParams.SortBy));

                return query;
            }

            return query;
        }
    }
}
