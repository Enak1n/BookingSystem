using BookingSystem.Domain.SeedWork;
using System.Linq.Expressions;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<List<Flight>> GetAllAsync(CancellationToken cancellationToken);
        Task<Flight> GetByIdAsync(Guid id);
        Task<List<Flight>> FindAsync(string departurePoint, string destinationPoint, DateTime departureDate);
    }
}
