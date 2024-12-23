using BookingSystem.Api.DTO;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> FindFlights([FromQuery]FindFlightsRequest findFlights)
        {
            var res = await _flightService.FindFlightsAsync(findFlights.DeparturePoint, findFlights.DestinationPoint, findFlights.DeparturedDate);

            return Ok(res);
        }
    }
}
