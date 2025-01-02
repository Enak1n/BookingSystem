using BookingSystem.Api.Options;
using Microsoft.Extensions.Options;
using Yandex.Checkout.V3;

namespace BookingSystem.Api.Utils
{
    public class PaymentClient
    {
        private readonly YooKassaSettings _configuration;

        public PaymentClient(IOptions<YooKassaSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        public void CreatePayment(double price)
        {
            var paymentClient = new Client(_configuration.ShopId, _configuration.ApiKey);

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

            var payment = paymentClient.CreatePayment(newPayment);
        }

    }
}
