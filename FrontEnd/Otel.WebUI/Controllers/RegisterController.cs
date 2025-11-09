using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.RegisterDTO;
using Otel.WebUI.DTOs.WorkLocationDTO;
using Microsoft.Extensions.Options;
using Otel.WebUI.Models;

namespace Otel.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
		private readonly string _apiUrl;

        public RegisterController(UserManager<AppUser> userManager, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _apiUrl = appSettings.Value.urlAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Items = await LoadWorkLocations();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = await LoadWorkLocations(); // load lại dropdown nếu model không hợp lệ
                return View(createUserDTO);
            }

            var appUser = new AppUser
            {
                Name = createUserDTO.Name,
                LastName = createUserDTO.LastName,
                UserName = createUserDTO.UserName,
                Email = createUserDTO.Email,
                PhoneNumber = createUserDTO.PhoneNumber,
                City = createUserDTO.City,
                Department = createUserDTO.Department,
                WorkLocationId = createUserDTO.WorkLocationId
            };

            var result = await _userManager.CreateAsync(appUser, createUserDTO.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Items = await LoadWorkLocations();
            return View(createUserDTO);
        }

        // Helper: load danh sách WorkLocations
        private async Task<List<SelectListItem>> LoadWorkLocations()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiUrl}/api/WorkLocation");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWorkLocationDTO>>(jsonData);

                return values?.Select(item => new SelectListItem
                {
                    Text = item.WorkLocationCityName,
                    Value = item.WorkLocationId.ToString()
                }).ToList() ?? new List<SelectListItem>();
            }

            return new List<SelectListItem>();
        }

    }
}
