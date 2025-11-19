using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.LoginDTO;
using Microsoft.Extensions.Logging;

namespace HanaHotel.WebUI.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly ILogger<LoginController> _logger;

		public LoginController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginController> logger)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(LoginUserDTO loginUserDTO)
		{
			if (!ModelState.IsValid)
				return View(loginUserDTO);

			var user = await _userManager.FindByNameAsync(loginUserDTO.UserName);
			if (user == null)
			{
				ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
				return View(loginUserDTO);
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginUserDTO.Password, false, false);
			if (!result.Succeeded)
			{
				// provide detailed feedback (locked/out/not allowed/2fa)
				if (result.IsLockedOut)
				{
					_logger.LogWarning("User {User} locked out", user.UserName);
					ModelState.AddModelError("", "Tài khoản bị khoá.");
				}
				else if (result.IsNotAllowed)
				{
					_logger.LogWarning("User {User} not allowed to sign in", user.UserName);
					ModelState.AddModelError("", "Tài khoản chưa được phép đăng nhập.");
				}
				else if (result.RequiresTwoFactor)
				{
					ModelState.AddModelError("", "Yêu cầu xác thực hai yếu tố.");
				}
				else
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
				}
				return View(loginUserDTO);
			}

			// Signed in successfully -> get roles from store and decide redirect
			var roles = await _userManager.GetRolesAsync(user);
			_logger.LogInformation("User {User} roles: {Roles}", user.UserName, roles != null ? string.Join(",", roles) : "(none)");

			// Save session info
			HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
			if (roles != null && roles.Any())
				HttpContext.Session.SetString("UserRole", roles.First());

			// Decide redirect based on roles list (case-insensitive)
			var isAdmin = roles?.Any(r => string.Equals(r, "Admin", StringComparison.OrdinalIgnoreCase)) ?? false;
			if (isAdmin)
				return RedirectToAction("Index", "AdminDashboard");

			return RedirectToAction("Index", "Default");
		}
	}
}
