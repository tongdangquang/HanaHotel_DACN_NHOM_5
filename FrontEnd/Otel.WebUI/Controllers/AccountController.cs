using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel.EntityLayer.Concrete;

namespace Otel.WebUI.Controllers
{
	[Authorize] // chỉ người đăng nhập mới truy cập được
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		// ✅ Trang thông tin tài khoản
		[HttpGet]
		public async Task<IActionResult> Info()
		{
			// Lấy tên người dùng từ session
			var username = HttpContext.Session.GetString("UserName");
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Index", "Login");
			}

			// Lấy thông tin user từ Identity, bao gồm WorkLocation nếu có
			var user = await _userManager.Users
				.Include(u => u.WorkLocation)
				.FirstOrDefaultAsync(u => u.UserName == username);

			if (user == null)
			{
				// Nếu session sai, quay lại đăng nhập
				HttpContext.Session.Clear();
				return RedirectToAction("Index", "Login");
			}

			ViewBag.UserName = user.UserName;
			ViewBag.Email = user.Email;
			ViewBag.Role = HttpContext.Session.GetString("UserRole");

			// Truyền model vào view để Info.cshtml không nhận null
			return View(user);
		}

		// ✅ Đăng xuất
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			// Đăng xuất khỏi Identity (xóa cookie)
			await _signInManager.SignOutAsync();

			// Xóa toàn bộ session
			HttpContext.Session.Clear();

			// Quay lại trang chủ
			return RedirectToAction("Index", "Default");
		}
	}
}
