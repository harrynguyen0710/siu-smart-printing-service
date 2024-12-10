using Microsoft.AspNetCore.Mvc;
using siu_smart_printing_service.Services;

namespace siu_smart_printing_service.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrintingLogController : Controller
    {
        private readonly PrintingLogService _printingLogService;
        public PrintingLogController(PrintingLogService printingLogService)
        {
            _printingLogService = printingLogService;
        }
        
        public async Task<IActionResult> Index()
        {
            var logs = await _printingLogService.GetAllAsync();
            return View(logs);
        }

        public async Task<IActionResult> Details(int printerId)
        {
            var logs = await _printingLogService.GetAllPrintingLogsIByPrinterId(printerId);
            return View(logs);
        }

        



    }
}
