using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.RegisterDTO;
using Otel.WebUI.DTOs.WorkLocationDTO;

namespace Otel.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(UserManager<AppUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
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
                ViewBag.Items = await LoadWorkLocations();
                return View(createUserDTO);
            }

            var appUser = new AppUser
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                LastName = createUserDTO.LastName,
                UserName = createUserDTO.UserName,
                PhoneNumber = createUserDTO.PhoneNumber,
                City = createUserDTO.City,
                Department = createUserDTO.Department,
                WorkLocationId = createUserDTO.WorkLocationId,
            };

            var result = await _userManager.CreateAsync(appUser, createUserDTO.Password);

            if (result.Succeeded)
                return RedirectToAction("Index", "Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            await LoadWorkLocations();
            return View(createUserDTO);
        }

        private async Task<List<SelectListItem>> LoadWorkLocations()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/WorkLocation");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWorkLocationDTO>>(jsonData);
                return values!.Select(item => new SelectListItem
                {
                    Text = item.WorkLocationCityName,
                    Value = item.WorkLocationId.ToString()
                }).ToList();
            }
            return [];
        }

    }
}
