using BookingSystem.BL.Models;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.BL.Filters
{
    public class FlightFilterPipeline
    {
        private readonly IEnumerable<IFlightFilter> _filters;

        public FlightFilterPipeline(IEnumerable<IFlightFilter> filters)
        {
            _filters = filters;
        }

        public async Task<List<FlightDto>> ApplyFilters(List<FlightDto> query, FlightFilterParams filterParams)
        {
            foreach (var filter in _filters)
            {
                query = await filter.FilterFlights(query, filterParams);
            }
            return query;
        }
    }
}
