namespace BookingSystem.Domain.SeedWork
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
