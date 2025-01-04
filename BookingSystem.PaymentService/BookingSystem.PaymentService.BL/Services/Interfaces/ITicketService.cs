using BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.PaymentService.Domain.TicketAggregate;

namespace BookingSystem.PaymentService.BL.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> AddAsync(Passenger passenger, Guid flightId);
    }
}
