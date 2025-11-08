using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.WebUI.DTOs.RoomDTO;
using Microsoft.AspNetCore.Authorization; // thêm namespace này

namespace Otel.WebUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7250/api/Room");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<List<ResultRoomDTO>>(jsonData);

            return View(rooms); // View nhận List<ResultRoomDTO>
        }

        // Chi tiết phòng
        [AllowAnonymous] // cho phép người chưa login xem
        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7250/api/Room/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<ResultRoomDTO>(jsonData);

            return View(room); // sẽ sử dụng Views/Room/Detail.cshtml
        }
    }
}