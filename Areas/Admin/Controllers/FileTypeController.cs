using Microsoft.AspNetCore.Mvc;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
