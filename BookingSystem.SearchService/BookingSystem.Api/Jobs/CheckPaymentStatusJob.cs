using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookingSystem.Api.Jobs
{
    public class CheckPaymentStatusJob 
    {
        //private readonly BookingContext _bookingContext;
        //private readonly PaymentClient _paymentClient;
        //private readonly ILogger<CheckPaymentStatusJob> _logger;

        //public CheckPaymentStatusJob(BookingContext bookingContext, PaymentClient paymentClient, ILogger<CheckPaymentStatusJob> logger)
        //{
        //    _bookingContext = bookingContext;
        //    _paymentClient = paymentClient;
        //    _logger = logger;
        //}

        //public async Task Execute(IJobExecutionContext context)
        //{
        //    var pendingPayments = await _bookingContext.PaymentStatuses
        //        .Where(x => x.Status == Status.Pending && x.PaymentEndDate > DateTime.UtcNow)
        //        .ToListAsync(context.CancellationToken);

        //    foreach (var payment in pendingPayments)
        //    {
        //        try
        //        {
        //            var isPaymentSuccessful = await _paymentClient.CheckPayment(payment.PaymentId);

        //            if (isPaymentSuccessful)
        //            {
        //                payment.Status = Status.Paid;
        //            }
        //            else if (DateTime.UtcNow >= payment.PaymentEndDate)
        //            {
        //                payment.Status = Status.Canceled;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"Error while checking payment status for PaymentId={payment.PaymentId}: {ex.Message}");
        //        }
        //    }

        //    await _bookingContext.SaveChangesAsync();
        //}
    }
}
