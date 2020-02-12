using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using WingsOn.Bll;
using WingsOn.Dal;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.ServicesTests
{
    public class BookingServiceTests
    {
        [Fact]
        public void CreateNewBooking_WithCorrectParameters_ShouldCreateNewBooking()
        {
            //Arrange
            var mockFlightRepository = new Mock<IRepository<Flight>>();
            var mockBookingRepository = new Mock<IRepository<Booking>>();
            var mockPersonRepository = new Mock<IRepository<Person>>();
            var testFlights = new[]{ new Flight
                {
                    Id = 30,
                    Number = "BB124",
                }
            };
            mockFlightRepository.Setup(repo => repo.GetAll()).Returns(testFlights);
            var testUser = new Person
            {
                Id = 100,
                Email = "egestas.lacinia@Proinmi.com",
                Gender = GenderType.Male,
                Name = "Branden Johnston"
            };
            mockPersonRepository.Setup(repo => repo.Get(It.Is<int>(i => i == testUser.Id))).Returns(testUser);
            var testBookings = new[]
            {
                new Booking
                {
                    Id = 55,
                    Number = "WO-291470",
                    Customer = testUser,
                    Flight = testFlights.First(),
                    Passengers = new[]
                    {
                        testUser
                    }
                }
            };
            mockBookingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(testBookings.First);
            mockBookingRepository.Setup(repo => repo.GetAll()).Returns(testBookings);
            BookingService bookingService = new BookingService(mockBookingRepository.Object, mockPersonRepository.Object, mockFlightRepository.Object);
            //Act
            var result = bookingService.CreateNewBooking("BB124", 100);
            //Assert
            Assert.NotNull(result);

        }
    }
}
