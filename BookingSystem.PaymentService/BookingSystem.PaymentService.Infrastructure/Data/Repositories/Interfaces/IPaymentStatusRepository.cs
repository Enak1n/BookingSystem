using BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.PaymentService.Domain.SeedWork;
using BookingSystem.PaymentService.Infrastructure.Entities;

namespace BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPaymentStatusRepository : IGenericRepository<PaymentStatus>
    {
        Task<List<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(PaymentStatus paymentStatus);
        Task AddRangeAsync(List<PaymentStatus> paymentStatuses);
        Task<List<PaymentStatus>> GetByStatusAsyn(Status status);
    }
}
