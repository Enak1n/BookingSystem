using BookingSystem.Domain.SeedWork;

namespace BookingSystem.Domain.AggregatesModel.PlaneAggregate
{
    public interface IPlaneRepository : IGenericRepository<Plane>
    {
        Task<List<Plane>> GetAllAsync();
        Task<Plane> GetByIdAsync(Guid id);
    }
}
