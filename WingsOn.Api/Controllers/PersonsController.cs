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
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IBookingService _bookingService;
        public PersonsController(IPersonService personService, IBookingService bookingService)
        {
            _personService = personService;
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_personService.GetPersonById(id));
        }

        [HttpPatch("{id}/email")]
        public ActionResult UpdatePersonsEmailAddress( int id, [FromForm] Person person)
        {
            return Ok(_personService.UpdatePersonsEmailAddress(id, person.Email));
        }
    }
}