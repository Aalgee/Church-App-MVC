using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
namespace LogicLayer
{
    /// <summary>
    /// interface for managing information going to the data access layer that pertains to the Booking table
    /// </summary>
    public interface IBookingManager
    {
        List<BookingVM> RetrieveBookingsByActive(bool active = true);
        bool EditBookingCheckOut(int bookingID, DateTime newCheckOut);
        bool EditBookingCheckIn(int bookingID, DateTime newChecIn);
        bool AddBooking(Booking booking);
        List<BookingVM> RetrieveBookingsByPersonID(int personID, bool active = true);
    }
}
