using BookingSystem.BL.Filters;
using BookingSystem.BL.Models;

namespace BookingSystem.SearchService.BL.Filters
{
    public class PriceFilter : IFlightFilter
    {
        public List<FlightDto> FilterFlights(List<FlightDto> query, FlightFilterParams filterParams)
        {
            if (filterParams.MinPrice > filterParams.MaxPrice || filterParams.MaxPrice < 0)
                throw new Exception("Проверьте правильность диапозона цены!");

            var flights = query.Where(x => x.Price >= filterParams.MinPrice && x.Price <= filterParams.MaxPrice).ToList();

            return flights;
        }
    }
}
