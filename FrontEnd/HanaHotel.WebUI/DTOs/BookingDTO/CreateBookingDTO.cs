using System;
using System.ComponentModel.DataAnnotations;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebUI.DTOs.BookingDTO
{
    public class CreateBookingDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        // Optional: server can set BookingDate when creating
        public DateTime? BookingDate { get; set; }

        public string? AdditionalRequest { get; set; }

        [Required]
        public int RoomId { get; set; }

        // If the request comes from a logged-in user, set UserId; otherwise can be null
        public int? UserId { get; set; }

        // Optional: allow client to suggest a status, otherwise server sets default (e.g., Pending)
        public BookingStatus? Status { get; set; }
    }
}
