using System;
using System.Collections.Generic;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public class FlightService:IFlightService
    {
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public FlightService(IRepository<Flight> flightRepository, IRepository<Booking> bookingRepository)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
        }
        public Flight GetFlightByNumber(string flightNumber)
        {
            var flight = _flightRepository.GetAll().FirstOrDefault(f => f.Number == flightNumber);
            if (flight == null)
            {
                throw new EntityNotFoundException($"There is no any flights with number {flightNumber}");
            }
            return flight;
        }

        public IEnumerable<Person> GetPersonsByFlight(string flightNumber)
        {
            Validator.CheckStringParameter(flightNumber);
            GetFlightByNumber(flightNumber);
            var persons = _bookingRepository.GetAll()
                .Where(booking => booking.Flight.Number == flightNumber)
                .SelectMany(b => b.Passengers).ToList();
            if (persons.Count == 0)
            {
                throw new EntityNotFoundException($"There is no any " +
                                                  $"passengers on the flight {flightNumber}");
            }

            return persons;
        }

        public IEnumerable<Person> GetPersonsByGender(GenderType type)
        {
            var persons = _bookingRepository.GetAll()
                .SelectMany(b => b.Passengers)
                .Where(p => p.Gender == type).ToList();
            if (persons.Count == 0)
            {
                throw new EntityNotFoundException("There is no any passengers of such gender");
            }

            return persons;
        }
    }
}