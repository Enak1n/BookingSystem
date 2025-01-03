namespace BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services
{
    public interface IPlaneService
    {
        Task<List<Plane>> GetAllPlanesAsync(CancellationToken cancellationToken);
        Task<Plane> GetPlaneByIdAsync(Guid id);
    }
}
