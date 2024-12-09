using BookingSystem.Domain.SeedWork;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<List<Flight>> GetAllAsync();
        Task<List<Flight>> GetByIdAsync(Guid id);
    }
}
