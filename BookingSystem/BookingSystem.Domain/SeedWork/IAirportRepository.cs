using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using System.Linq.Expressions;

namespace BookingSystem.Domain.SeedWork
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        Task<List<Airport>> FindAsync(Expression<Func<Airport, bool>> predicate);
        Task<Airport> GetByIdAsync(Guid id);
    }
}
