using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using BookingSystem.Domain.SeedWork;

namespace BookingSystem.BL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<List<Flight>> FindFlightsAsync(string departurePoint, string destinationPoint, DateTime departureDate)
        {
            var flights = await _flightRepository.FindAsync(departurePoint, destinationPoint, departureDate);

            return flights;
        }

        public async Task<Flight> GetInfoAboutFlightAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            
            return flight;
        }
    }
}
