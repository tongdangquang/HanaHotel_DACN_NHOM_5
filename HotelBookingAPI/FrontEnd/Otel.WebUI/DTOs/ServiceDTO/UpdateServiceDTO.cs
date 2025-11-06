using System.ComponentModel.DataAnnotations;

namespace Otel.WebUI.DTOs.ServiceDTO
{
    public class UpdateServiceDTO
    {
        [Required(ErrorMessage = "Service ID is required.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Service icon is required.")]
        [MaxLength(255, ErrorMessage = "Service icon URL cannot exceed 255 characters.")]
        public string ServiceIcon { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }
    }
}
