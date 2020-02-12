using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using WingsOn.Bll;
using WingsOn.Dal;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.ServicesTests
{
    public class FlightServiceTests
    {
        [Fact]
        public void GetPersonsByFlight_WithIncorrectFlightNumber_ShouldThrowArgumentException()
        {
            //Arrange
            var mockFlightRepository = new Mock<IRepository<Flight>>();
            var mockBookingRepository = new Mock<IRepository<Booking>>();
            var testFlightNumber = "   ";
            var flightService = new FlightService(mockFlightRepository.Object, mockBookingRepository.Object);
            //Act&Assert
            Assert.Throws<ArgumentException>(() => flightService.GetPersonsByFlight(testFlightNumber));
        }

        [Fact]
        public void GetPersonsByFlight_WithNonExistingFlightNumber_ShouldThrowEntityNotFoundException()
        {
            //Arrange
            var mockFlightRepository = new Mock<IRepository<Flight>>();
            var mockBookingRepository = new Mock<IRepository<Booking>>();
            var testFlightNumber = "BB1244";
            var testFlight = new []{ new Flight
            {
                Id = 30,
                Number = "BB124",
            }
            };
            mockFlightRepository.Setup(repo => repo.GetAll()).Returns(testFlight);
            var flightService = new FlightService(mockFlightRepository.Object, mockBookingRepository.Object);
            //Act&Assert
            Assert.Throws<EntityNotFoundException>(() => flightService.GetPersonsByFlight(testFlightNumber));
        }
    }
}
