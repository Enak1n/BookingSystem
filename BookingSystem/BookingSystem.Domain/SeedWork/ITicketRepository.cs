using BookingSystem.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.Domain.SeedWork
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task RemoveAsync(Guid id);
    }
}
