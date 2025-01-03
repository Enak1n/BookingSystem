using AutoMapper;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
using BookingSystem.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        private readonly BookingContext _bookingContext;
        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork => _bookingContext;

        public PlaneRepository(BookingContext context, IMapper mapper)
        {
            _bookingContext = context;
            _mapper = mapper;
        }

        public async Task<List<Plane>> GetAllAsync(CancellationToken cancellationToken)
        {
            var planes = await _bookingContext.Planes.AsNoTracking().ToListAsync(cancellationToken);

            var result = _mapper.Map<List<Plane>>(planes);

            return result;
        }

        public async Task<Plane> GetByIdAsync(Guid id)
        {
            var plane = await _bookingContext.Planes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<Plane>(plane);

            return result;
        }
    }
}
