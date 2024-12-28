namespace BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services
{
    public interface IAirportService
    {
        Task<List<Airport>> GetAllAsync(CancellationToken cancellationToken);
        Task<Airport> GetByIdAsync(Guid id);
    }
}
