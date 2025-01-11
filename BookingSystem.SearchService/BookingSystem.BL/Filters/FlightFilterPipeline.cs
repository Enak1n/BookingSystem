using BookingSystem.BL.Models;

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
            var flights = new List<FlightDto>(query);

            foreach (var filter in _filters)
            {
                flights = filter.FilterFlights(query, filterParams);
            }

            return flights;
        }
    }
}
