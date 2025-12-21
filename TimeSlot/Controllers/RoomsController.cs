using Microsoft.AspNetCore.Mvc;
using TimeSlot.Persistence;

namespace TimeSlot.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            var rooms = _roomRepository.GetAll();
            return View(rooms);
        }
    }
}
