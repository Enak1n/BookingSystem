using BookingSystem.BL.Models;

namespace BookingSystem.BL.Filters
{
    public class SortByFilter : IFlightFilter
    {
        public List<FlightDto> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams)
        {
            var flights = new List<FlightDto>(query);
            if (!string.IsNullOrEmpty(filterParams.SortBy))
            {
                flights = filterParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DepartureDate).ToList()
                    : query.OrderBy(x => x.DepartureDate).ToList();

                return query;
            }

            return flights;
        }
    }
}
