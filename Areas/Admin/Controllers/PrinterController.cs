using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Areas.Admin.Services;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    public class PrinterController : Controller
    {
        private readonly PrinterService _printerService;
        public PrinterController(PrinterService printerService)
        {
            _printerService = printerService;
        }

        public IActionResult Index()
        {
            var printers = _printerService.GetAllPrinters();
            return View(printers);
        }
    }
}
