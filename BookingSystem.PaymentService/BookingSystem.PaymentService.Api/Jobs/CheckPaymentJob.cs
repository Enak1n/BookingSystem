using BookingSystem.PaymentService.Api.DTO;
using BookingSystem.PaymentService.Api.Utils;
using BookingSystem.PaymentService.BL.Services.Interfaces;
using BookingSystem.PaymentService.Domain.TicketAggregate;
using BookingSystem.PaymentService.Infrastructure.Data.Repositories.Interfaces;
using BookingSystem.PaymentService.Infrastructure.Entities;
using MessageBus;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Quartz;

namespace BookingSystem.PaymentService.Api.Jobs
{
    public class CheckPaymentJob : IJob
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly ILogger<CheckPaymentJob> _logger;
        private readonly KafkaMessageBus _messageBus;
        private readonly ITicketService _ticketService;
        private readonly PaymentClient _paymentClient;
        private readonly IDistributedCache _cache;

        public CheckPaymentJob(ILogger<CheckPaymentJob> logger, KafkaMessageBus messageBus,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient, ITicketService ticketService,
            IDistributedCache cache)
        {
            _paymentStatusRepository = paymentStatusRepository;
            _logger = logger;
            _messageBus = messageBus;
            _paymentClient = paymentClient;
            _ticketService = ticketService;
            _cache = cache;
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
                {
                    var res = await _cache.GetStringAsync(payment.PaymentId);

                    if (res is null)
                        return;

                    var deserializedObject = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(res);

                    var passenger = Passenger.Create(deserializedObject.Passenger.Name, deserializedObject.Passenger.Surname,
                        deserializedObject.Passenger.Patronymic, deserializedObject.Passenger.Email);

                    await _ticketService.AddAsync(passenger, payment.FlightId);
                    payment.Status = Status.Paid;
                }
            });

            await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
