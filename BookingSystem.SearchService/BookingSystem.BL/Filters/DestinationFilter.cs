using BookingSystem.BL.Models;

namespace BookingSystem.BL.Filters
{
    public class DestinationFilter : IFlightFilter
    {
        public List<FlightDto> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams)
        {
            var flights = new List<FlightDto>(query);
            if (!string.IsNullOrEmpty(filterParams.Destination))
            {
                flights = query.Where(f => f.DestinationPoint.Contains(filterParams.Destination)).ToList();
            }

            return flights;
        }
    }
}
