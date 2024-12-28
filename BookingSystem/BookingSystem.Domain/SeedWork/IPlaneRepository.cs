using BookingSystem.Domain.AggregatesModel.PlaneAggregate;

namespace BookingSystem.Domain.SeedWork
{
    public interface IPlaneRepository : IGenericRepository<Plane>
    {
        Task<List<Plane>> GetAllAsync(CancellationToken cancellationToken);
        Task<Plane> GetByIdAsync(Guid id);
    }
}
