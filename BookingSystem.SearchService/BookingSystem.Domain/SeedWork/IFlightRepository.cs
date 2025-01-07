using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.Domain.SeedWork
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<List<Flight>> GetAllAsync(CancellationToken cancellationToken);
        Task<Flight> GetByIdAsync(Guid id);
        Task<List<Flight>> FindAsync(string departurePoint, string destinationPoint, DateTime departureDate);
        Task UpdateAsync(Flight entity);
    }
}
