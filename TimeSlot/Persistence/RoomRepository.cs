using TimeSlot.Data;
using TimeSlot.Models;

namespace TimeSlot.Persistence
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TimeSlotContext _context;


        public RoomRepository(TimeSlotContext context)
        {
            _context = context;
        }


        public void Add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var room = GetById(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();


            }

        }

        public List<Room> GetAll()
        {
            return _context.Rooms.ToList();   
            
        }

        public Room? GetById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            
        }

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
           

        }
    }
}
