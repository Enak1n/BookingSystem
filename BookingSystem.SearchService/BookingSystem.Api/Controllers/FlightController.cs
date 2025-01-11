using BookingSystem.Api.DTO;
using BookingSystem.Api.DTO.Flights;
using BookingSystem.BL.Models;
using BookingSystem.BL.Services.Interfaces;
using BookingSystem.Domain.AggregatesModel.TicketAggregate.Services;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.SearchService.Api.Extensions;

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
            _searchFlightsService = searchFlightsService;
        }

        [HttpGet]
        public async Task<IActionResult> FindFlights([FromQuery] FindFlightsRequest findFlights)
        {
            var flights = await _flightService.FindFlightsAsync(findFlights.DeparturePoint, findFlights.DestinationPoint, findFlights.DeparturedDate);

            var res = flights.Select(flight => flight.ToDto());

            return Ok(res);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterFlights(FilterRequest filterRequest, CancellationToken cancellationToken)
        {
            var filtered = await _searchFlightsService.FilterFlightsAsync(filterRequest.Flights, filterRequest.FilterParams, cancellationToken);

            return Ok(filtered);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoAboutFlight(Guid id)
        {
            var flight = await _flightService.GetInfoAboutFlightAsync(id);

            var res = flight.ToDto();

            return Ok(res);
        }
    }
}
