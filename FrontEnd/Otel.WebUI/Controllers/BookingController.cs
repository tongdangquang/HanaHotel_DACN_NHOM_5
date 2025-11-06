using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.BookingDTO;
using System.Text;

namespace Otel.WebUI.Controllers
{
    [AllowAnonymous]

    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult AddBooking()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDTO createBookingDto)
        {
            createBookingDto.Status = BookingStatus.Pending;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:44355/api/Booking", stringContent);
            return RedirectToAction("Index", "Default");
        }

    }
}
