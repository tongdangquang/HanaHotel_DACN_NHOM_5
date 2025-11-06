using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Otel.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
