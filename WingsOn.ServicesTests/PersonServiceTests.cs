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
    public class PersonServiceTests
    {

        [Fact]
        public void GetPersonById_WithCorrectId_ShouldReturnNeededPerson()
        {
            //Arrange
            var mock = new Mock<IRepository<Person>>();
            var testId = 100;
            var testUser = new Person
            {
                Id = 100,
                Address = "P.O. Box 795, 1956 Odio. Rd.",
                DateBirth = DateTime.Now,
                Email = "egestas.lacinia@Proinmi.com",
                Gender = GenderType.Male,
                Name = "Branden Johnston"
            };
            mock.Setup(repo => repo.Get(It.Is<int>(i=>i==testUser.Id))).Returns(testUser);
            var personService = new PersonService(mock.Object);

            //Act
            var result = personService.GetPersonById(testId);

            //Assert
            result.Should().BeEquivalentTo(testUser);
        }

        [Fact]
        public void GetPersonById_WithIncorrectId_ShouldThrowArgumentException()
        {
            //Arrange
            var mock = new Mock<IRepository<Person>>();
            var testId = -100;
            var personService = new PersonService(mock.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => personService.GetPersonById(testId));

        }

        [Fact]
        public void GetPersonById_WithNonExistingId_ShouldThrowEntityNotFoundException()
        {
            //Arrange
            var mock = new Mock<IRepository<Person>>();
            var testId = 10;
            var personService = new PersonService(mock.Object);
            mock.Setup(repo => repo.Get(It.Is<int>(i => i == testId))).Returns((Person) null);

            //Act&Assert
            Assert.Throws<EntityNotFoundException>(() => personService.GetPersonById(testId));

        }

        [Fact]
        public void UpdatePersonsEmailAddress_WithCorrectParameters_ShouldReturnUpdatedPerson()
        {
            //Arrange
            var mock = new Mock<IRepository<Person>>();
            var testId = 100;
            var email = "test@email.com";
            Queue<Person> testUsers = new Queue<Person>(new[]
            {
                new Person
                {
                    Id = 100,
                    Address = "P.O. Box 795, 1956 Odio. Rd.",
                    DateBirth = DateTime.Now,
                    Email = "egestas.a.dui@aliquet.ca",
                    Gender = GenderType.Male,
                    Name = "Branden Johnston"
                },
            
                new Person
                {
                    Id = 100,
                    Address = "P.O. Box 795, 1956 Odio. Rd.",
                    DateBirth = DateTime.Now,
                    Email = "test@email.com",
                    Gender = GenderType.Male,
                    Name = "Branden Johnston"
                }
            });

            mock.Setup(repo => repo.Get(It.Is<int>(i => i == testId))).Returns(testUsers.Dequeue);
            var personService = new PersonService(mock.Object);
            //Act
            var result = personService.UpdatePersonsEmailAddress(testId, email);
            //Assert
            Assert.Equal(result.Email, email);
        }

        [Fact]
        public void UpdatePersonsEmailAddress_WithIncorrectEmail_ShouldThrowArgumentException()
        {
            //Arrange
            var mock = new Mock<IRepository<Person>>();
            var email = "jahflkalfjkj";
            var id = 100;
            var testUser = new Person
            {
                Id = 100,
                Address = "P.O. Box 795, 1956 Odio. Rd.",
                DateBirth = DateTime.Now,
                Email = "egestas.lacinia@Proinmi.com",
                Gender = GenderType.Male,
                Name = "Branden Johnston"
            };
            mock.Setup(repo => repo.Get(It.Is<int>(i => i == testUser.Id))).Returns(testUser);
            var personService = new PersonService(mock.Object);
            //Act&Assert
            Assert.Throws<ArgumentException>(() => personService.UpdatePersonsEmailAddress(id, email));
        }
    }
}