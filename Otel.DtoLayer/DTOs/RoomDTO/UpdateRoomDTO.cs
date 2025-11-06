using System.ComponentModel.DataAnnotations;

namespace Otel.DtoLayer.DTOs.RoomDTO
{
    public class UpdateRoomDTO
    {
        [Required(ErrorMessage = "Room ID is required.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room number is required.")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Room cover image is required.")]
        public string RoomCoverImage { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bed count is required.")]
        public string BedCount { get; set; }

        [Required(ErrorMessage = "Bath count is required.")]
        public string BathCount { get; set; }

        [Required(ErrorMessage = "Wi-Fi availability is required.")]
        public string Wifi { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}