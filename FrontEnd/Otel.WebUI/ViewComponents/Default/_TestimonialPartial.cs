using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.WebUI.DTOs.TestimonialDTO;

namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _TestimonialPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TestimonialPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/Testimonial");

            if (!responseMessage.IsSuccessStatusCode)
                return View("Error");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var testimonialData = JsonConvert.DeserializeObject<List<ResultTestimonialDTO>>(jsonData);

            if (testimonialData == null || testimonialData.Count == 0)
                return View("Empty");

            return View(testimonialData);
        }
    }
}
