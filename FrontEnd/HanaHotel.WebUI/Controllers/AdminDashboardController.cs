using Microsoft.AspNetCore.Mvc;

namespace HanaHotel.WebUI.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
