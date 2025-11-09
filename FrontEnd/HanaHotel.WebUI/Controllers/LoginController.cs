using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.LoginDTO;

namespace HanaHotel.WebUI.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index() => View();

		[HttpPost]
		public async Task<IActionResult> Index(LoginUserDTO loginUserDTO)
		{
			if (!ModelState.IsValid)
				return View(loginUserDTO);

			var user = await _userManager.FindByNameAsync(loginUserDTO.UserName);
			if (user == null)
			{
				ModelState.AddModelError("", "Tài khoản không tồn tại");
				return View(loginUserDTO);
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginUserDTO.Password, false, false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Đăng nhập không hợp lệ");
				return View(loginUserDTO);
			}

			// Lưu session username
			HttpContext.Session.SetString("UserName", user.UserName);

			// Lấy tất cả role (nếu cần hiển thị)
			var roles = await _userManager.GetRolesAsync(user);
			if (roles != null && roles.Any())
				HttpContext.Session.SetString("UserRole", roles.First());

			// Kiểm tra membership an toàn
			var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

			if (isAdmin)
				return RedirectToAction("Index", "AdminDashboard");

			return RedirectToAction("Index", "Default");
		}
	}
}
