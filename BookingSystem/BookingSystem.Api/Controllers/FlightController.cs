using BookingSystem.Api.DTO;
using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Domain.AggregatesModel.TicketAggregate;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace BookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ISearchFlightsService _searchFlightsService;

        public FlightController(IFlightService flightService, ISearchFlightsService searchFlightsService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> FindFlights([FromQuery] FindFlightsRequest findFlights)
        {
            var res = await _flightService.FindFlightsAsync(findFlights.DeparturePoint, findFlights.DestinationPoint, findFlights.DeparturedDate);

            Response.Cookies.Append("flights", JsonConvert.SerializeObject(res));
            return Ok(res);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterFlights(FlightFilterParams filterRequest, CancellationToken cancellationToken)
        {
            var json = Request.Cookies.TryGetValue("flights", out string flights);

            var flights1 = JsonConvert.DeserializeObject<List<Flight>>(flights);

            var filtered = await _searchFlightsService.FilterFlightsAsync(flights1, filterRequest, cancellationToken);

            return Ok(filtered);
        }
    }
}
