using BookingSystem.PaymentService.Domain.SeedWork;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using BookingSystem.PaymentService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.PaymentService.Infrastructure.Data.Repositories
{
    public class PaymentStatusReopository : IPaymentStatusRepository
    {
        private readonly BookingContext _bookingContext;

        public IUnitOfWork UnitOfWork => _bookingContext;

        public PaymentStatusReopository(BookingContext bookingContext)
        {
            _bookingContext = bookingContext;
        }

        public async Task AddAsync(PaymentStatus paymentStatus)
        {
            await _bookingContext.AddAsync(paymentStatus);
        }

        public async Task<List<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken)
        {
            var statuses = await _bookingContext.PaymentStatuses.ToListAsync(cancellationToken);

            return statuses;
        }

        public async Task<List<PaymentStatus>> GetByStatusAsyn(Status status)
        {
            var statuses = await _bookingContext.PaymentStatuses.Where(x => x.Status == status).ToListAsync();

            return statuses;
        }
    }
}
