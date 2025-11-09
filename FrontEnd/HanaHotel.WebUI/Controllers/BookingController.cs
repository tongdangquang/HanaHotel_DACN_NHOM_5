using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.BookingDTO;
using HanaHotel.WebUI.DTOs.ServiceDTO;
using HanaHotel.WebUI.DTOs.RoomDTO;
using System.Text;
using Microsoft.Extensions.Options;
using HanaHotel.WebUI.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace HanaHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings, ILogger<BookingController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrl = appSettings.Value.urlAPI;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        [HttpGet]
        public async Task<PartialViewResult> AddBooking()
        {
            await FillRoomAndServiceListsAsync();
            var model = new CreateBookingDTO();
            // return the specific partial by name
            return PartialView("_AddBookingPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDTO createBookingDto)
        {
            createBookingDto.Status = BookingStatus.Pending;

            // If user is logged in, try to set UserId from claims
            var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var uid))
            {
                createBookingDto.UserId = uid;
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var resp = await client.PostAsync($"{_apiUrl}/api/Booking", stringContent);
                if (!resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    _logger.LogWarning("Booking POST failed. Status: {Status}, Body: {Body}", resp.StatusCode, body);
                    ModelState.AddModelError("", "Unable to create booking. Please try again.");
                    // refill lists then return partial by name with the model so the view renders
                    await FillRoomAndServiceListsAsync();
					return PartialView("_AddBookingPartial", createBookingDto);
				}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when posting booking to {ApiUrl}", _apiUrl);
                ModelState.AddModelError("", "An error occurred while sending booking.");
                // refill lists then return partial by name with the model so the view renders
                await FillRoomAndServiceListsAsync();
                return PartialView("_AddBookingPartial", createBookingDto);
            }

            return RedirectToAction("Index", "Default");
        }

        // Helper: load lists and log failures so view always has lists (not null)
        private async Task FillRoomAndServiceListsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            ViewBag.Services = new List<ResultServiceDTO>();
            ViewBag.Rooms = new List<ResultRoomDTO>();

            // Load services
            try
            {
                var svcUrl = $"{_apiUrl}/api/Service";
                var svcResp = await client.GetAsync(svcUrl);
                if (svcResp.IsSuccessStatusCode)
                {
                    var json = await svcResp.Content.ReadAsStringAsync();
                    var services = JsonConvert.DeserializeObject<List<ResultServiceDTO>>(json) ?? new List<ResultServiceDTO>();
                    ViewBag.Services = services;
                }
                else
                {
                    var body = await svcResp.Content.ReadAsStringAsync();
                    _logger.LogWarning("GET {Url} returned {Status}. Body: {Body}", svcUrl, svcResp.StatusCode, body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching services from {ApiUrl}", _apiUrl);
            }

            // Load rooms
            try
            {
                var roomUrl = $"{_apiUrl}/api/Room";
                var roomResp = await client.GetAsync(roomUrl);
                if (roomResp.IsSuccessStatusCode)
                {
                    var json = await roomResp.Content.ReadAsStringAsync();
                    var rooms = JsonConvert.DeserializeObject<List<ResultRoomDTO>>(json) ?? new List<ResultRoomDTO>();
                    ViewBag.Rooms = rooms;
                }
                else
                {
                    var body = await roomResp.Content.ReadAsStringAsync();
                    _logger.LogWarning("GET {Url} returned {Status}. Body: {Body}", roomUrl, roomResp.StatusCode, body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching rooms from {ApiUrl}", _apiUrl);
            }
        }
    }
}
