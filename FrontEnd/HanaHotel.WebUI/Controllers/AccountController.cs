using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.UserDTO;

namespace HanaHotel.WebUI.Controllers
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

		// Trang thông tin tài khoản
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

		// Đăng xuất
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

        // GET: show edit form for currently logged-in user
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Index", "Login");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var model = new EditAccountDTO
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = user.City,
                Department = user.Department
            };

            return View(model);
        }

        // POST: save edits
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAccountDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy người dùng.");
                return View(model);
            }

            // Update fields
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;
            user.Department = model.Department;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(model);
            }

            return RedirectToAction("Info");
        }
	}
}
