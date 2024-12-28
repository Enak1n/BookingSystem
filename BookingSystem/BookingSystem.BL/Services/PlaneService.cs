using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services;
using BookingSystem.Domain.Exceptions;
using BookingSystem.Domain.SeedWork;

namespace BookingSystem.BL.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly IPlaneRepository _planeRepository;

        public PlaneService(IPlaneRepository planeRepository)
        {
            _planeRepository = planeRepository;
        }

        public Task<List<Plane>> GetAllPlanesAsync(CancellationToken cancellationToken)
        {
            var planes = _planeRepository.GetAllAsync(cancellationToken);

            return planes;
        }

        public Task<Plane> GetPlaneByIdAsync(Guid id)
        {
            var plane = _planeRepository.GetByIdAsync(id);

            if (plane is null)
                throw new NotFoundException($"Самолет с {id} не найден!");

            return plane;
        }
    }
}
