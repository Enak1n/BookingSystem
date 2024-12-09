using BookingSystem.Domain.AggregatesModel.PlaneAggregate.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneService _planeService;

        public PlaneController(IPlaneService planeService)
        {
            _planeService = planeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _planeService.GetAllPlanesAsync();

            return Ok(res);
        }
    }
}
