using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.WebUI.DTOs.AboutDTO;
namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _AboutUsPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AboutUsPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/About");

            if (!responseMessage.IsSuccessStatusCode)
                return View("Error");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var aboutData = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData);

            if (aboutData == null || aboutData.Count == 0)
                return View("Empty");

            return View(aboutData[0]);
        }
    }
}



