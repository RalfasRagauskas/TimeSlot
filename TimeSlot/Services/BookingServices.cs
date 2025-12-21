using TimeSlot.Models;
using TimeSlot.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSlot.Services
{
    public class BookingService : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public List<Booking> GetAll()
        {
            return _bookingRepository.GetAll();
        }

        public Booking? GetById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public BookingResult Add(Booking booking)
        {
            // Krav 1: sluttid > starttid
            if (booking.EndTime <= booking.StartTime)
                return new BookingResult
                {
                    Key = "EndTime",
                    ErrorMessage = "End time cannot be before start time",
                    IsSuccessful = false
                };

            // Krav 2: starttid i fremtiden
            if (booking.StartTime < DateTime.Now)
                return new BookingResult
                {
                    Key = "StartTime",
                    ErrorMessage = "Start time cannot be in the past",
                    IsSuccessful = false
                };

            // Krav 3: ingen overlap med eksisterende bookinger
            var existingBookings = _bookingRepository.GetAll()
                .Where(b => b.RoomId == booking.RoomId);

            foreach (var existing in existingBookings)
            {
                bool overlap = booking.StartTime < existing.EndTime &&
                               booking.EndTime > existing.StartTime;
                if (overlap)
                    return new BookingResult
                    {
                        Key = "RoomId",
                        ErrorMessage = "Room is not available for the selected time",
                        IsSuccessful = false
                    };
            }

            _bookingRepository.Add(booking);
            return new BookingResult { IsSuccessful = true };
        }


        public BookingResult Update(Booking booking)
        {
            
            if (booking.EndTime <= booking.StartTime)
                return new BookingResult
                {
                    Key = "EndTime",
                    ErrorMessage = "End time cannot be before start time",
                    IsSuccessful = false
                };

            
            if (booking.StartTime < DateTime.Now)
                return new BookingResult
                {
                    Key = "StartTime",
                    ErrorMessage = "Start time cannot be in the past",
                    IsSuccessful = false
                };

            
            var existingBookings = _bookingRepository.GetAll()
                .Where(b => b.RoomId == booking.RoomId && b.BookingId != booking.BookingId);

            foreach (var existing in existingBookings)
            {
                bool overlap = booking.StartTime < existing.EndTime &&
                               booking.EndTime > existing.StartTime;
                if (overlap)
                    return new BookingResult
                    {
                        Key = "RoomId",
                        ErrorMessage = "Room is not available for the selected time",
                        IsSuccessful = false
                    };
            }

            _bookingRepository.Update(booking);
            return new BookingResult { IsSuccessful = true };
        }

        public void Delete(int id)
        {
            _bookingRepository.Delete(id);
        }

        private bool IsValidBooking(Booking booking, int? bookingId = null)
        {
            if (booking.EndTime <= booking.StartTime) return false;
            if (booking.StartTime < DateTime.Now) return false;

            var existingBookings = _bookingRepository.GetAll()
                .Where(b => b.RoomId == booking.RoomId);

            if (bookingId != null)
                existingBookings = existingBookings.Where(b => b.BookingId != bookingId);

            foreach (var existing in existingBookings)
            {
                bool overlap = booking.StartTime < existing.EndTime &&
                               booking.EndTime > existing.StartTime;
                if (overlap) return false;
            }

            return true;
        }
    }
}
