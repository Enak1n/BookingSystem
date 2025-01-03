using BookingSystem.PaymentService.Api.Options;
using Microsoft.Extensions.Options;
using Yandex.Checkout.V3;

namespace BookingSystem.PaymentService.Api.Utils
{
    public class PaymentClient
    {
        private readonly YooKassaSettings _configuration;
        private readonly Client _paymentClient;
        private readonly ILogger<PaymentClient> _logger;

        public PaymentClient(IOptions<YooKassaSettings> configuration, ILogger<PaymentClient> logger)
        {
            _configuration = configuration.Value;
            _paymentClient = new Client(_configuration.ShopId, _configuration.ApiKey);
            _logger = logger;
        }

        public async Task<string> CreatePaymentAsync(double price)
        {
            var asyncClient = _paymentClient.MakeAsync();

            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = Convert.ToDecimal(price), Currency = "RUB" },
                Confirmation = new Confirmation
                {
                    Type = ConfirmationType.Redirect,
                    ReturnUrl = ""
                },
                Capture = true
            };

            var payment = await asyncClient.CreatePaymentAsync(newPayment);

            return payment.Id;
        }

        public async Task<bool> CheckPayment(string paymentId)
        {
            var asyncClient = _paymentClient.MakeAsync();

            try
            {
                var paymentInfo = await asyncClient.GetPaymentAsync(paymentId);

                return paymentInfo.Status == PaymentStatus.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while checking payment status {ex.Message}");
                return false;
            }
        }
    }
}
