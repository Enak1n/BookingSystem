namespace BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services
{
    public interface IAirportService
    {
        Task<List<Airport>> GetAllAsync();
        Task<Airport> GetByIdAsync(Guid id);
    }
}
