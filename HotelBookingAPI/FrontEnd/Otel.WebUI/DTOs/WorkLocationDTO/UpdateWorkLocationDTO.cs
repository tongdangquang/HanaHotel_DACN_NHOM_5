using System.ComponentModel.DataAnnotations;

namespace Otel.WebUI.DTOs.WorkLocationDTO
{
    public class UpdateWorkLocationDTO
    {
        [Required(ErrorMessage = "Work Location ID is required.")]
        public int WorkLocationId { get; set; }

        [Required(ErrorMessage = "City name is required.")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
        public string WorkLocationCityName { get; set; } = null!;

        [Required(ErrorMessage = "Country name is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string WorkLocationCountry { get; set; } = null!;
    }
}
