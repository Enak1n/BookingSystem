namespace BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services
{
    public interface IPlaneService
    {
        Task<List<Plane>> GetAllPlanesAsync();
        Task<Plane> GetPlaneByIdAsync(Guid id);
    }
}
