using BookingSystem.BL.Services.Interfaces;
using BookingSystem.SearchService.Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("createPayment")]
        public async Task<IActionResult> Test(CreatePaymentDto createPaymentDto)
        {
            await _paymentService.CreatePayment(createPaymentDto.FlightId, createPaymentDto.Passenger);

            return Ok();
        }
    }
}
