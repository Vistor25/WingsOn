using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Flight> _flightRepository;
        public BookingService(IRepository<Booking> bookingRepository, 
            IRepository<Person> personRepository, 
            IRepository<Flight> flightRepository)
        {
            _bookingRepository = bookingRepository;
            _personRepository = personRepository;
            _flightRepository = flightRepository;
        }

        public Booking CreateNewBooking(string flightNumber, int personId)
        {
            Validator.CheckIdParameter(personId);
            Validator.CheckStringParameter(flightNumber);
            var person = _personRepository.Get(personId);
            if (person == null)
            {
                throw new EntityNotFoundException($"Person with id = {personId} is not found");
            } ;
            var flight = _flightRepository.GetAll().FirstOrDefault(f => f.Number == flightNumber);
            if (flight == null)
            {
                throw new EntityNotFoundException($"Flight with number = {flightNumber} is not found");
            };
            var bookingIds = _bookingRepository.GetAll().Select(b => b.Id).ToList();
            Random rd = new Random();
            int id = rd.Next(1, Int32.MaxValue);
            while (bookingIds.Contains(id))
            {
                id = rd.Next();
            }
            Booking booking = new Booking
            {
                Id = id,
                Number = "WO-" + rd.Next(100000, 999999),
                Customer = person,
                DateBooking = DateTime.Now,
                Flight = flight,
                Passengers = new[]
                {
                    person,
                }
            };
            _bookingRepository.Save(booking);
            var createdBooking = _bookingRepository.Get(id);
            if (createdBooking == null)
            {
                throw new Exception("Couldn't create new booking");
            }
            return createdBooking;

        }
    }
}
