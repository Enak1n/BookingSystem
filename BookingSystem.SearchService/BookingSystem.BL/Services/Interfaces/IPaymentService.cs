using BookingSystem.BL.Models;
using BookingSystem.SearchService.BL.Models;

namespace BookingSystem.BL.Services.Interfaces
{
    public interface IPaymentService
    {
        Task CreatePayment(Guid flightId, PassengerBrokerDto passenger);
    }
}
