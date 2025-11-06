using Microsoft.AspNetCore.Mvc;

namespace Otel.WebUI.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
