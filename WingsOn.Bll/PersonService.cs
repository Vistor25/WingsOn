using System;
using System.Threading.Tasks;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }
        public Person GetPersonById(int id)
        {
            Validator.CheckIdParameter(id);
            var person = _personRepository.Get(id);
            if (person == null)
            {
                throw new EntityNotFoundException($"Person with id = {id} is not found");
            }
            return person;
        }

        public Person UpdatePersonsEmailAddress(int personId, string email)
        {
            Validator.CheckIdParameter(personId);
            Validator.CheckEmailParameter(email);
            var personForUpdate = _personRepository.Get(personId);
            if (personForUpdate == null)
            {
                throw new EntityNotFoundException($"Person with id = {personId} is not found");
            }
            personForUpdate.Email = email;
            _personRepository.Save(personForUpdate);
            var personUpdated = _personRepository.Get(personForUpdate.Id);
            if (personUpdated.Email != email)
            {
                throw new Exception("Couldn't update passenger's email address");
            }

            return personUpdated;
        }
    }
}
