using BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate;

namespace BookingSystem.PaymentService.Domain.SeedWork
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task RemoveAsync(Guid id);
    }
}
