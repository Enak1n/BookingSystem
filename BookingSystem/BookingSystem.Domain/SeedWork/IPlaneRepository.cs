using BookingSystem.Domain.AggregatesModel.PlaneAggregate;

namespace BookingSystem.Domain.SeedWork
{
    public interface IPlaneRepository : IGenericRepository<Plane>
    {
        Task<Plane> GetByIdAsync(Guid id);
    }
}
