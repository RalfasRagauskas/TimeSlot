using TimeSlot.Data;
using TimeSlot.Models;

namespace TimeSlot.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TimeSlotContext _context;

        //start her
        public void Add(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetAll()
        {
            throw new NotImplementedException();
        }

        public Booking? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
