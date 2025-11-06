using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.UserDTO;
using Otel.WebUI.DTOs.WorkLocationDTO;
using Microsoft.Extensions.Options;
using Otel.WebUI.Models;

namespace Otel.WebUI.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
		private readonly string _apiUrl;

		public AdminUserController(UserManager<AppUser> userManager, IMapper mapper, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _apiUrl = appSettings.Value.urlAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Include(user => user.WorkLocation).ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            ViewBag.Items = await LoadWorkLocations();
            var value = _mapper.Map<UpdateUserDTO>(user);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = await LoadWorkLocations();
                return View(updateUserDTO);
            }

            var user = await _userManager.FindByIdAsync(updateUserDTO.Id.ToString());
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                ViewBag.Items = await LoadWorkLocations();
                return View(updateUserDTO);
            }

            user.Name = updateUserDTO.Name;
            user.LastName = updateUserDTO.LastName;
            user.UserName = updateUserDTO.UserName;
            user.Email = updateUserDTO.Email;
            user.PhoneNumber = updateUserDTO.PhoneNumber;
            user.City = updateUserDTO.City;
            user.Department = updateUserDTO.Department;
            user.WorkLocationId = updateUserDTO.WorkLocationId;

            if (!string.IsNullOrEmpty(updateUserDTO.Password))
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUserDTO.Password);

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user!);
            return RedirectToAction("Index", "AdminUser");
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
            return [];
        }
    }
}
