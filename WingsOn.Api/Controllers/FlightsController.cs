using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Bll;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("{flightNumber}/passengers")]
        public ActionResult GetPersonsByFlight(string flightNumber)
        {
            var result = _flightService.GetPersonsByFlight(flightNumber);
            return Ok(result);
        }

        [HttpGet]
        [Route("passengers")]
        public ActionResult GetPersonByGender(GenderType gender)
        {
            var result = _flightService.GetPersonsByGender(gender);
            return Ok(result);
        }
    }
}