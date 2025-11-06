using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.WebUI.DTOs.ServiceDTO;
namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _ServicePartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _ServicePartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/Service");

            if (!responseMessage.IsSuccessStatusCode)
                return View("Error");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var aboutData = JsonConvert.DeserializeObject<List<ResultServiceDTO>>(jsonData);

            if (aboutData == null || aboutData.Count == 0)
                return View("Empty");

            var roomsToDisplay = aboutData.Take(6).ToList(); // İlk 6 veriyi al


            return View(roomsToDisplay);
        }
    }
}
