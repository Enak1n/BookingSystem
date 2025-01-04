using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Entities.Outbox;
using BookingSystem.SearchService.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.SearchService.Infrastructure.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly BookingContext _bookingContext;

        public IUnitOfWork UnitOfWork => _bookingContext;

        public MessageRepository(BookingContext bookingContext)
        {
            _bookingContext = bookingContext;
        }

        public async Task<List<BrokerMessage>> GetAllAsync(CancellationToken cancellationToken)
        {
            var messages = await _bookingContext.Messages.ToListAsync(cancellationToken);

            return messages;
        }

        public async Task<List<BrokerMessage>> GetByStatusAsync(Status status)
        {
            var messages = await _bookingContext.Messages.Where(x => x.Status == status).ToListAsync();

            return messages;
        }

        public async Task AddAsync(BrokerMessage message)
        {
            await _bookingContext.Messages.AddAsync(message);
        }
    }
}
