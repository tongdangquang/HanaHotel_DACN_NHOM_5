using Microsoft.AspNetCore.Mvc;

namespace HanaHotel.WebUI.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Gi?i thi?u - Hana Hotel";
            return View();
        }
    }
}