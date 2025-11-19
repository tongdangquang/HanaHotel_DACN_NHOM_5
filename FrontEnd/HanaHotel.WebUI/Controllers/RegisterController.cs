using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.RegisterDTO;

namespace HanaHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RegisterController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserDTO);
            }

            var appUser = new User
            {
                Name = createUserDTO.Name,
                DateOfBirth = createUserDTO.DateOfBirth,
                Gender = createUserDTO.Gender,
                Address = createUserDTO.Address,
                Phone = createUserDTO.Phone,
                Email = createUserDTO.Email,
                UserName = createUserDTO.UserName,
                // RoleId left default; role is assigned by RoleManager below
            };

            var result = await _userManager.CreateAsync(appUser, createUserDTO.Password);

            if (result.Succeeded)
            {
                var customerRoleName = "Customer";

                if (!await _roleManager.RoleExistsAsync(customerRoleName))
                {
                    var createRoleResult = await _roleManager.CreateAsync(new Role { Name = customerRoleName });
                    if (!createRoleResult.Succeeded)
                    {
                        foreach (var error in createRoleResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                        return View(createUserDTO);
                    }
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(appUser, customerRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    foreach (var error in addToRoleResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(createUserDTO);
                }

                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(createUserDTO);
        }
    }
}
