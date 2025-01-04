using BookingSystem.PaymentService.BL.Services.Interfaces;
using BookingSystem.PaymentService.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.PaymentService.Domain.SeedWork;
using BookingSystem.PaymentService.Domain.TicketAggregate;
using BookingSystem.PaymentService.Infrastructure.Entities;

namespace BookingSystem.PaymentService.BL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> AddAsync(Passenger passenger, Guid flightId)
        {
            var ticket = Ticket.Create(Guid.NewGuid(), passenger);

            await _ticketRepository.CreateAsync(ticket, flightId);
            await _ticketRepository.UnitOfWork.SaveChangesAsync();

            return ticket;
        }
    }
}
