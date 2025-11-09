using Microsoft.AspNetCore.Authorization; // thêm namespace này
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HanaHotel.WebUI.DTOs.RoomDTO;
using Microsoft.Extensions.Options;
using HanaHotel.WebUI.Models;

namespace HanaHotel.WebUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
		private readonly string _apiUrl;

		public RoomController(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrl = appSettings.Value.urlAPI;
		}

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiUrl}/api/Room");

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
            var response = await client.GetAsync($"{_apiUrl}/api/Room/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<ResultRoomDTO>(jsonData);

            return View(room); // sẽ sử dụng Views/Room/Detail.cshtml
        }
    }
}