namespace Otel.WebUI.DTOs.WorkLocationDTO
{
    public class ResultWorkLocationDTO
    {
        public int WorkLocationId { get; set; }
        public string WorkLocationCityName { get; set; } = null!;
        public string WorkLocationCountry { get; set; } = null!;
    }
}
