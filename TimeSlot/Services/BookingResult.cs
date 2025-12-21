namespace TimeSlot.Services
{
    public class BookingResult
    {
        public string Key { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        public bool IsSuccessful { get; set; }
    }
}
