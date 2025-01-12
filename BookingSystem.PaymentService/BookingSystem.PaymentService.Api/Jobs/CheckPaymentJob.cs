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
        private readonly ITicketService _ticketService;
        private readonly PaymentClient _paymentClient;
        private readonly IDistributedCache _cache;

        public CheckPaymentJob(ILogger<CheckPaymentJob> logger,
            IPaymentStatusRepository paymentStatusRepository, PaymentClient paymentClient, ITicketService ticketService,
            IDistributedCache cache)
        {
            _paymentStatusRepository = paymentStatusRepository;
            _logger = logger;
            _paymentClient = paymentClient;
            _ticketService = ticketService;
            _cache = cache;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var payments = await _paymentStatusRepository.GetByStatusAsyn(Status.Pending);

            await Parallel.ForEachAsync(payments, async (payment, _) =>
            {
                await ProcessPaymentAsync(payment);
            });

            await _paymentStatusRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task ProcessPaymentAsync(PaymentStatus payment)
        {
            var paymentResult = await _paymentClient.CheckPayment(payment.PaymentId);

            if (!paymentResult)
                return;

            if (DateTime.UtcNow > payment.PaymentEndDate && !paymentResult)
            {
                payment.Status = Status.Canceled;
                return;
            }

            var cachedData = await _cache.GetStringAsync(payment.PaymentId);
            if (cachedData is null)
            {
                _logger.LogWarning($"Платеж {payment.PaymentId} не найден в кэше");
                return;
            }

            var paymentDto = JsonConvert.DeserializeObject<CreatePaymentMessageDto>(cachedData);
            if (paymentDto is null)
            {
                _logger.LogError($"Не удалось десериализовать данные для платежа {payment.PaymentId}");
                return;
            }

            var passenger = Passenger.Create(
                paymentDto.Passenger.Name,
                paymentDto.Passenger.Surname,
                paymentDto.Passenger.Patronymic,
                paymentDto.Passenger.Email);

            await _ticketService.AddAsync(passenger, payment.FlightId);
            payment.Status = Status.Paid;
        }
    }
}
