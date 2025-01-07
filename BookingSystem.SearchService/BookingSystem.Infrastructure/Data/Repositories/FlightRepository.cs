using AutoMapper;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly BookingContext _bookingContext;
        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork => _bookingContext;

        public FlightRepository(BookingContext context, IMapper mapper)
        {
            _bookingContext = context;
            _mapper = mapper;
        }

        public async Task<List<Flight>> FindAsync(string departurePoint, string destinationPoint, DateTime departureDate)
        {
            var flightsEntity = await _bookingContext.Flights.Where(x => x.DeparturePoint == departurePoint
                                                                         && x.DestinationPoint == destinationPoint
                                                                         && DateTime.UtcNow < departureDate).Include(x => x.Plane).ToListAsync();

            var flights = _mapper.Map<List<Flight>>(flightsEntity);

            return flights;
        }

        public async Task<List<Flight>> GetAllAsync(CancellationToken cancellationToken)
        {
            var flightsEntity = await _bookingContext.Flights.AsNoTracking().ToListAsync(cancellationToken);
            var flights = _mapper.Map<List<Flight>>(flightsEntity);

            return flights;
        }

        public async Task<Flight> GetByIdAsync(Guid id)
        {
            var flightEntity = await _bookingContext.Flights
                .Include(x => x.DepartureAirport)
                .Include(x => x.DestinationAirport)
                .Include(x => x.Plane)
                .FirstOrDefaultAsync(f => f.Id == id);

            var flight = _mapper.Map<Flight>(flightEntity);

            return flight;
        }

        public async Task UpdateAsync(Flight entity)
        {
            await _bookingContext.Flights.Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(f =>
                f.SetProperty(x => x.EmptyPlaces, entity.EmptyPlaces)
                .SetProperty(x => x.DepartureDate, entity.DepartureDate));
        }
    }
}
