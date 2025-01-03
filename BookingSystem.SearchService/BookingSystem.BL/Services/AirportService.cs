using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services;
using BookingSystem.Domain.SeedWork;

namespace BookingSystem.BL.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _repository;

        public AirportService(IAirportRepository airportRepository)
        {
            _repository = airportRepository;
        }

        public async Task<List<Airport>> GetAllAsync(CancellationToken cancellationToken)
        {
            var airports = await _repository.GetAllAsync(cancellationToken);

            return airports;
        }

        public async Task<Airport> GetByIdAsync(Guid id)
        {
            var airport = await _repository.GetByIdAsync(id);

            return airport;
        }
    }
}
