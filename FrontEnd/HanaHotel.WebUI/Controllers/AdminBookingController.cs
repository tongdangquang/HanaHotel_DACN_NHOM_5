using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.BookingDTO;
using HanaHotel.WebUI.Models;
using HanaHotel.WebUI.ViewModels;

namespace HanaHotel.WebUI.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
		private readonly string _apiUrl;

		public AdminBookingController(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpClientFactory;
			_apiUrl = appSettings.Value.urlAPI;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiUrl}/api/Booking");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDTO>>(jsonData);
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiUrl}/api/Booking/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<UpdateBookingDTO>(jsonData);
                return View(booking);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDTO updateBookingDTO)
        {
            var client = _httpClientFactory.CreateClient();

            var existingBookingResponse = await client.GetAsync($"{_apiUrl}/api/Booking/{updateBookingDTO.BookingId}");

            if (!existingBookingResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Không thể lấy thông tin chi tiết của đơn đặt phòng.");
                return View(updateBookingDTO);
            }

            var existingBookingJson = await existingBookingResponse.Content.ReadAsStringAsync();
            var existingBooking = JsonConvert.DeserializeObject<Booking>(existingBookingJson);

            existingBooking!.CheckInDate = updateBookingDTO.CheckInDate;
            existingBooking.CheckOutDate = updateBookingDTO.CheckOutDate;
            existingBooking.Status = updateBookingDTO.Status;

            // 3. Güncellenmiş entity'yi API'ye gönder
            var updatedContent = new StringContent(JsonConvert.SerializeObject(existingBooking), System.Text.Encoding.UTF8, "application/json");
            var updateResponse = await client.PutAsync($"{_apiUrl}/api/Booking", updatedContent);

            if (updateResponse.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Không thể cập nhật đơn đặt phòng. Vui lòng thử lại.");
            return View(updateBookingDTO);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiUrl}/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi xóa đơn đặt phòng.");
            return View();
        }
    }

}
