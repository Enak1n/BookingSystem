using BookingSystem.PaymentService.Api.Utils;
using BookingSystem.PaymentService.Infrastructure.Data;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookingSystem.PaymentService.Api.Jobs
{
    public class CheckPaymentJob : IJob
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly ILogger<CheckPaymentJob> _logger;
        private readonly KafkaMessageBus _messageBus;
        private readonly PaymentClient _paymentClient;

        public CheckPaymentJob(ILogger<CheckPaymentJob> logger, KafkaMessageBus messageBus,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient)
        {
            _paymentStatusRepository = paymentStatusRepository;
            _logger = logger;
            _messageBus = messageBus;
            _paymentClient = paymentClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var payments = await _paymentStatusRepository.GetByStatusAsyn(Status.Pending);

            await Parallel.ForEachAsync(payments, async (payment, _) =>
            {
                if (DateTime.UtcNow > payment.PaymentEndDate)
                    payment.Status = Status.Canceled;

                var paymentResult = await _paymentClient.CheckPayment(payment.PaymentId);

                if (paymentResult)
                    payment.Status = Status.Paid;
            });

            await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
