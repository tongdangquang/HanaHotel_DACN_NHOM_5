using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.UserDTO;
using System.Threading.Tasks;

namespace HanaHotel.WebUI.Controllers
{
	[Authorize] // only authenticated users can access
	public class AccountController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		// Account info page
		[HttpGet]
		public async Task<IActionResult> Info()
		{
			var username = HttpContext.Session.GetString("UserName");
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Index", "Login");
			}

			// Use Identity APIs to fetch user
			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				// Session may be invalid — clear and redirect to login
				HttpContext.Session.Clear();
				return RedirectToAction("Index", "Login");
			}

			ViewBag.UserName = user.UserName;
			ViewBag.Email = user.Email;
			ViewBag.Role = HttpContext.Session.GetString("UserRole");

			return View(user);
		}

		// Logout
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Default");
		}

		// GET: show edit form for currently logged-in user
		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			var username = HttpContext.Session.GetString("UserName");
			if (string.IsNullOrEmpty(username))
				return RedirectToAction("Index", "Login");

			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				HttpContext.Session.Clear();
				return RedirectToAction("Index", "Login");
			}

			// Map User -> EditAccountDTO (align with current DTO)
			var model = new EditAccountDTO
			{
				Id = user.Id,
				Name = user.Name,                      // DTO.Name expected to hold full name
				DateOfBirth = user.DateOfBirth,
				Gender = user.Gender,
				Address = user.Address,
				Phone = user.Phone,
				Email = user.Email,
				UserName = user.UserName,
				RoleId = user.RoleId
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
				ModelState.AddModelError(string.Empty, "User not found.");
				return View(model);
			}

			// Map fields from DTO to User
			user.Name = model.Name;
			user.DateOfBirth = model.DateOfBirth;
			user.Gender = model.Gender;
			user.UserName = model.UserName;
			user.Email = model.Email;
			user.Phone = model.Phone;
			user.Address = model.Address;
			user.RoleId = model.RoleId;

			if (!string.IsNullOrEmpty(model.Password))
			{
				// Hash password and set PasswordHash
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
			}

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
					ModelState.AddModelError(string.Empty, err.Description);
				return View(model);
			}

			// Optionally update session username/email if changed
			HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
			if (!string.IsNullOrEmpty(user.RoleId.ToString()))
			{
				HttpContext.Session.SetString("UserRole", HttpContext.Session.GetString("UserRole") ?? string.Empty);
			}

			return RedirectToAction("Info");
		}
	}
}
