using Microsoft.AspNetCore.Mvc;

namespace Otel.WebUI.Controllers
{
    public class AdminCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
