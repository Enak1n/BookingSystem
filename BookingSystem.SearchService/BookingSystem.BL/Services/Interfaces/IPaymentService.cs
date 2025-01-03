using BookingSystem.BL.Models;

namespace BookingSystem.BL.Services.Interfaces
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto createPaymentDto);
    }
}
