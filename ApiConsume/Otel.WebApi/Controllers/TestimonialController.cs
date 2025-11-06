using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public IActionResult GetTestimonials()
        {
            var testimonials = _testimonialService.TGetList();
            return Ok(testimonials);
        }

        [HttpGet("{id}")]
        public IActionResult GetTestimonial(int id)
        {
            var testimonial = _testimonialService.TGetByID(id);
            return Ok(testimonial);
        }

        [HttpPut]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            _testimonialService.TUpdate(testimonial);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddTestimonial(Testimonial testimonial)
        {
            _testimonialService.TInsert(testimonial);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            var testimonial = _testimonialService.TGetByID(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            _testimonialService.TDelete(testimonial);
            return NoContent();
        }
    }
}
