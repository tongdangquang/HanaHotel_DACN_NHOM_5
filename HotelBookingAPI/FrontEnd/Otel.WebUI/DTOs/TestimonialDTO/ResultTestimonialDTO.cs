namespace Otel.WebUI.DTOs.TestimonialDTO
{
    public class ResultTestimonialDTO
    {
        public int TestimonialId { get; set; }

        public required string Name { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
    }
}
