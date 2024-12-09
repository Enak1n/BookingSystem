using BookingSystem.Domain.SeedWork;
using System.Linq.Expressions;

namespace BookingSystem.Domain.AggregatesModel.PlaceAggregate
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        Task<List<Airport>> GetAllAsync();
        Task<List<Airport>> FindAsync(Expression<Func<Airport, bool>> predicate);
        Task<Airport> GetByIdAsync(Guid id);
    }
}
