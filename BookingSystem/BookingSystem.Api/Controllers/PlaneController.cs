using BookingSystem.Domain.AggregatesModel.PlaceAggregate.Services;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaneController : ControllerBase
    {
        private readonly IAirportService _planeService;

        public PlaneController(IAirportService planeService)
        {
            _planeService = planeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _planeService.GetAllAsync();

            return Ok(res);
        }
    }
}
