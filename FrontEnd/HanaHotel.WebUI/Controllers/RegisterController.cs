using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.RegisterDTO;
using HanaHotel.WebUI.DTOs.WorkLocationDTO;
using Microsoft.Extensions.Options;
using HanaHotel.WebUI.Models;

namespace HanaHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpClientFactory _httpClientFactory;
		private readonly string _apiUrl;

        public RegisterController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
            {
                // Ensure "Customer" role exists
                var customerRoleName = "Customer";
                if (!await _roleManager.RoleExistsAsync(customerRoleName))
                {
                    var createRoleResult = await _roleManager.CreateAsync(new AppRole { Name = customerRoleName });
                    if (!createRoleResult.Succeeded)
                    {
                        foreach (var error in createRoleResult.Errors)
                            ModelState.AddModelError("", error.Description);

                        ViewBag.Items = await LoadWorkLocations();
                        return View(createUserDTO);
                    }
                }

                // Assign user to "Customer" role
                var addToRoleResult = await _userManager.AddToRoleAsync(appUser, customerRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    foreach (var error in addToRoleResult.Errors)
                        ModelState.AddModelError("", error.Description);

                    ViewBag.Items = await LoadWorkLocations();
                    return View(createUserDTO);
                }

                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            // Gán lại items trước khi trả view để dropdown không bị mất
            ViewBag.Items = await LoadWorkLocations();
            return View(createUserDTO);
        }

        private async Task<List<SelectListItem>> LoadWorkLocations()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiUrl}/api/WorkLocation");
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
            // Trả về list rỗng hợp lệ thay vì cú pháp không hợp lệ
            return new List<SelectListItem>();
        }

    }
}
