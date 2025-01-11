using BookingSystem.BL.Filters;
using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;

namespace BookingSystem.BL.Services
{
    public class SearchFlightsService : ISearchFlightsService
    {
        private readonly FlightFilterPipeline _filterPipeline;

        public SearchFlightsService(FlightFilterPipeline flightFilterPipeline)
        {
            _filterPipeline = flightFilterPipeline;
        }

        public async Task<List<FlightDto>> FilterFlightsAsync(List<FlightDto> flights, FlightFilterParams filterParams, CancellationToken cancellationToken)
        {
            var query = await _filterPipeline.ApplyFilters(flights, filterParams);

            return query;
        }
    }
}
