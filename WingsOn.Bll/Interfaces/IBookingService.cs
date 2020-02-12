using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsOn.Bll
{
    public interface IBookingService
    {
       Booking CreateNewBooking(string flightNumber, int personId);
    }
}
