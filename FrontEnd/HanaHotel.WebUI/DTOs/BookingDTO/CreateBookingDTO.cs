using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebUI.DTOs.BookingDTO
{
    public class CreateBookingDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Room { get; set; }
        public string SpecialRequest { get; set; }
        public BookingStatus Status { get; set; }

        // new properties for relations
        public int? ServiceId { get; set; }
        public int? RoomId { get; set; }
        public int? UserId { get; set; }
    }
}
