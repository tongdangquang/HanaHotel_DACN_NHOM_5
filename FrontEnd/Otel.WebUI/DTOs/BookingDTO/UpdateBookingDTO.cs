using Otel.EntityLayer.Concrete;

namespace Otel.WebUI.DTOs.BookingDTO
{
    public class UpdateBookingDTO
    {
        public int BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
    }
}
