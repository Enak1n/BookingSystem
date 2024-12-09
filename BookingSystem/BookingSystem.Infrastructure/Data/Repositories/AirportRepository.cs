using AutoMapper;
using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using BookingSystem.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingSystem.Infrastructure.Data.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly BookingContext _bookingContext;
        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork => _bookingContext;

        public AirportRepository(BookingContext bookingContext, IMapper mapper)
        {
            _bookingContext = bookingContext;
            _mapper = mapper;
        }

        public Task<List<Airport>> FindAsync(Expression<Func<Airport, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Airport>> GetAllAsync()
        {
            var airports = await _bookingContext.Airports.AsNoTracking().
                Include(x => x.Country).ToListAsync();

            var res = _mapper.Map<List<Airport>>(airports);

            return res;
        }

        public async Task<Airport> GetByIdAsync(Guid id)
        {
            var airportEntity = await _bookingContext.Airports.AsNoTracking().
                Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == id);

            var airport = _mapper.Map<Airport>(airportEntity);

            return airport;
        }
    }
}
