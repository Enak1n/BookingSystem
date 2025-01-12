﻿using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using BookingSystem.Domain.SeedWork;

namespace BookingSystem.BL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<List<Flight>> FindFlightsAsync(string departurePoint, string destinationPoint, DateTime departureDate)
        {
            var flights = await _flightRepository.FindAsync(departurePoint, destinationPoint, departureDate);

            return flights;
        }

        public async Task<Flight> GetInfoAboutFlightAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            
            return flight;
        }

        public async Task TakeASeat(Guid flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            flight.TakeASeat();

            await _flightRepository.UpdateAsync(flight);
            await _flightRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task ReturnASeat(Guid flightId)
        {
            await _lock.WaitAsync();
            try
            {
                var flight = await _flightRepository.GetByIdAsync(flightId);
                flight.ReturnASeat();
                await _flightRepository.UpdateAsync(flight);
                await _flightRepository.UnitOfWork.SaveChangesAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
