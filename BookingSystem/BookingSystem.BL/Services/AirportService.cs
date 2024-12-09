using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services;

namespace BookingSystem.BL.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _repository;

        public AirportService(IAirportRepository airportRepository)
        {
            _repository = airportRepository;
        }

        public async Task<List<Airport>> GetAllAsync()
        {
            var airports = await _repository.GetAllAsync();

            return airports;
        }

        public async Task<Airport> GetByIdAsync(Guid id)
        {
            var airport = await _repository.GetByIdAsync(id);

            return airport;
        }
    }
}
