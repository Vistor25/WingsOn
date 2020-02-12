using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public interface IFlightService
    {
        Flight GetFlightByNumber(string flightNumber);
        IEnumerable<Person> GetPersonsByFlight(string flightNumber);
        IEnumerable<Person> GetPersonsByGender(GenderType type);
    }
}