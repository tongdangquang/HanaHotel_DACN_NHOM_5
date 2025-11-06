namespace Otel.WebUI.DTOs.ServiceDTO
{
    public class ResultServiceDTO
    {
        public int ServiceId { get; set; }
        public required string ServiceIcon { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
