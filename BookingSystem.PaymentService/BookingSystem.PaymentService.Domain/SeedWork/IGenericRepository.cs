namespace BookingSystem.PaymentService.Domain.SeedWork
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
