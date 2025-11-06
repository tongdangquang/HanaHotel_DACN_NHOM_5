namespace Otel.EntityLayer.Concrete
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Room { get; set; }
        public string SpecialRequest { get; set; }
        public BookingStatus Status { get; set; }

    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }


}
