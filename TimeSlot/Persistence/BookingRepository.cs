using Microsoft.EntityFrameworkCore;
using TimeSlot.Data;
using TimeSlot.Models;

namespace TimeSlot.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TimeSlotContext _context;

        public BookingRepository(TimeSlotContext context)
        {
            _context = context;
        }

      
        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
              

        }

        public void Delete(int id)
        {
            var booking = GetById(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

        }

        public List<Booking> GetAll()
        {
            return _context.Bookings
                  .Include(b => b.Room) // Her henter EF også Room
                  .ToList();

        }

        public Booking? GetById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.BookingId == id);

        }

        public void Update(Booking booking)
        {
            // Hent først den eksisterende booking fra DB
            var existing = _context.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
            if (existing != null)
            {
                
                existing.Title = booking.Title;
                existing.StartTime = booking.StartTime;
                existing.EndTime = booking.EndTime;
                existing.RoomId = booking.RoomId;

                _context.SaveChanges();
            }
        }

    }
}
