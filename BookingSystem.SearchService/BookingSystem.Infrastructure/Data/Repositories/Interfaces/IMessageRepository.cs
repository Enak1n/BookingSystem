using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Entities.Outbox;

namespace BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<BrokerMessage>
    {
        Task<List<BrokerMessage>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<BrokerMessage>> GetByStatusAsync(Status status);
        Task AddAsync(BrokerMessage message);
    }
}
