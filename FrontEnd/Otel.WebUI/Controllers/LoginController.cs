using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.LoginDTO;

namespace Otel.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid)
                return View(loginUserDTO);

            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.UserName, loginUserDTO.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "AdminDashboard");
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(loginUserDTO);
            }
        }
    }
}
