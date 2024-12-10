using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Areas.Admin.Services;

namespace siu_smart_printing_service.Controllers
{
    [Authorize]
    public class PrinterController : Controller
    {
        private readonly PrinterService _printerService;
        public PrinterController(PrinterService printerService)
        {
            _printerService = printerService;
        }
        public async Task<IActionResult> Index()
        {
            var printers = await _printerService.GetAllPrinters();
            return View(printers);
        }
    }
}
