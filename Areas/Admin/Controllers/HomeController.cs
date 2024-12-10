using Microsoft.AspNetCore.Mvc;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
