using System.ComponentModel.DataAnnotations;

namespace Otel.WebUI.DTOs.ServiceDTO
{
    public class CreateServiceDTO
    {
        private const string LENGTH_MESSAGE = "Title can be must maximum 50 characters";

        [Required(ErrorMessage = "Service icon is required.")]
        public string ServiceIcon { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = LENGTH_MESSAGE)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
