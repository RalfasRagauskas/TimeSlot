using TimeSlot.Models;
using System.Collections.Generic;

namespace TimeSlot.Services
{
    public interface IBookingServices
    {
        List<Booking> GetAll();
        Booking? GetById(int id);

        BookingResult Add(Booking booking);
        BookingResult Update(Booking booking);
        void Delete(int id);
    }
}
