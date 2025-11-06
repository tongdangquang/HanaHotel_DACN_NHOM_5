using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.Models.Role;

namespace Otel.WebUI.Controllers
{
    public class AdminRoleAssignController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AdminRoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.Users.ToListAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return RedirectToAction("Error404", "ErrorPage");
            }

            TempData["user_id"] = user.Id;

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleAssignViewModels = roles.Select(role => new RoleAssignViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name!,
                RoleExist = userRoles.Contains(role.Name!)
            }).ToList();

            return View(roleAssignViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(List<RoleAssignViewModel> roleAssignViewModels)
        {
            var user_id = (int)TempData["user_id"]!;
            var user = await _userManager.FindByIdAsync(user_id.ToString());
            foreach (var item in roleAssignViewModels)
                if (item.RoleExist)
                    await _userManager.AddToRoleAsync(user!, item.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user!, item.RoleName);
            return RedirectToAction("Index");
        }
    }
}
