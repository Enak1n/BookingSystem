﻿using AutoMapper;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Domain.SeedWork;
using BookingSystem.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BookingContext _bookingContext;
        private readonly IMapper _mapper;

        public TicketRepository(BookingContext bookingContext, IMapper mapper)
        {
            _bookingContext = bookingContext;
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _bookingContext;

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            var ticketEntity = new TicketEntity
            {
                Id = ticket.Id,
                Passenger = new PassengerEntity
                {
                    Email = ticket.Passenger.Email,
                    Name = ticket.Passenger.Name,
                    Patronymic = ticket.Passenger.Patronymic,
                    Surname = ticket.Passenger.Surname,
                },
                Seat = 1,
                FlightId = ticket.Fligth.Id
            };

            await _bookingContext.Tickets.AddAsync(ticketEntity);

            return ticket;
        }

        public async Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken)
        {
            var ticketsEntity = await _bookingContext.Tickets.AsNoTracking().ToListAsync(cancellationToken);

            var tickets = _mapper.Map<List<Ticket>>(ticketsEntity);

            return tickets;
        }

        public async Task RemoveAsync(Guid id)
        {
            await _bookingContext.Tickets.Where(e => e.Id == id).ExecuteDeleteAsync();
        }
    }
}