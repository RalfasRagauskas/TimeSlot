using Microsoft.AspNetCore.Mvc;
using TimeSlot.Models;
using TimeSlot.Persistence;
using TimeSlot.Services;
using TimeSlot.ViewModels;

namespace TimeSlot.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;
        private readonly IRoomRepository _roomRepository;

        public BookingsController(IBookingServices bookingServices, IRoomRepository roomRepository)
        {
            _bookingServices = bookingServices;
            _roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            var bookings = _bookingServices.GetAll();
            return View(bookings);
        }

        public IActionResult Add(int? id)
        {
            ViewBag.Action = "add";

            var bookingVM = new BookingViewModel
            {
                Rooms = _roomRepository.GetAll()
            };

            var date = DateTime.Now;
            bookingVM.Booking.StartTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            bookingVM.Booking.EndTime = new DateTime(date.Year, date.Month, date.Day, date.Hour + 1, date.Minute, 0);

            if (id != null)
                bookingVM.Booking.RoomId = id.Value;

            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Add(BookingViewModel bookingVM)
        {
            if (!ModelState.IsValid)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "add";
                return View(bookingVM);
            }

            var result = _bookingServices.Add(bookingVM.Booking);

            if (!result.IsSuccessful)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "add";

                // Key skal matche partialen: Booking.StartTime, Booking.EndTime, Booking.RoomId
                ModelState.AddModelError("Booking." + result.Key, result.ErrorMessage);

                return View(bookingVM);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = _bookingServices.GetById(id.Value);
            if (booking == null) return NotFound();

            BookingViewModel bookingVM = new BookingViewModel
            {
                Booking = booking,
                Rooms = _roomRepository.GetAll()
            };

            ViewBag.Action = "edit";
            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Edit(BookingViewModel bookingVM)
        {
            if (!ModelState.IsValid)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "edit";
                return View(bookingVM);
            }

            var result = _bookingServices.Update(bookingVM.Booking);

            if (!result.IsSuccessful)
            {
                bookingVM.Rooms = _roomRepository.GetAll();
                ViewBag.Action = "edit";

                // Key skal matche partialen
                ModelState.AddModelError("Booking." + result.Key, result.ErrorMessage);

                return View(bookingVM);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _bookingServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

