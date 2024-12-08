using Microsoft.AspNetCore.Mvc;

namespace siu_smart_printing_service.Controllers
{
    public class UploadedFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
