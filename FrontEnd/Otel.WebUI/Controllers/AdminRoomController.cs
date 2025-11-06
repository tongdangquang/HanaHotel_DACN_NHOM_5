using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.WebUI.DTOs.RoomDTO;
using System.Text;

namespace Otel.WebUI.Controllers
{
    public class AdminRoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminRoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/Room");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultRoomDTO>>(jsonData);
                return View(values);
            }
            return View(new List<ResultRoomDTO>());
        }

        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(AddRoomDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44355/api/Room", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "An error occurred while adding the room.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44355/api/Room/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateRoomDTO>(jsonData);

                if (values != null)
                    return View(values);

                ModelState.AddModelError(string.Empty, "No room data found.");
            }
            else
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving room data.");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:44355/api/Room", stringContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "An error occurred while updating the room.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:44355/api/Room/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "An error occurred while deleting the room.");
            return RedirectToAction("Index");
        }
    }
}
