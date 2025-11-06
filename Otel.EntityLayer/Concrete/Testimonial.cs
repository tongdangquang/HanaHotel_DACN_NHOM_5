namespace Otel.EntityLayer.Concrete
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }

        public required string Name { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
    }
}