using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public interface IPersonService
    {
        Person GetPersonById(int id);
        Person UpdatePersonsEmailAddress(int personID, string personAddress);
    }
}
